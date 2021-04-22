// © Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.R9.Generators.Metric
{
    internal class Parser
    {
        private static readonly Regex _regex = new Regex("^[A-Z]+[A-za-z0-9]*$", RegexOptions.Compiled);
        private static readonly Regex _regexDimensionNames = new Regex("^[A-Za-z]+[A-Za-z0-9_.:-]*$", RegexOptions.Compiled);
        private static readonly char[] _separators = new[] { ',', ' ' };

        private readonly CancellationToken _cancellationToken;
        private readonly Compilation _compilation;
        private readonly Action<Diagnostic> _reportDiagnostic;

        public Parser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            _compilation = compilation;
            _cancellationToken = cancellationToken;
            _reportDiagnostic = reportDiagnostic;
        }

        public IReadOnlyList<MetricInstrumentClass> GetCounterClasses(IEnumerable<ClassDeclarationSyntax> classes)
        {
            const string CounterAttribute = "Microsoft.R9.Extensions.Meter.Int64CounterMetricAttribute";
            const string ValueRecorderAttribute = "Microsoft.R9.Extensions.Meter.Int64ValueRecorderMetricAttribute";

            var counterAttribute = _compilation.GetTypeByMetadataName(CounterAttribute);
            if (counterAttribute is null)
            {
                // nothing to do if this type isn't available
                return Array.Empty<MetricInstrumentClass>();
            }

            var valueRecorderAttribute = _compilation.GetTypeByMetadataName(ValueRecorderAttribute);
            if (valueRecorderAttribute is null)
            {
                // nothing to do if this type isn't available
                return Array.Empty<MetricInstrumentClass>();
            }

            var meterSymbol = _compilation.GetTypeByMetadataName("Microsoft.R9.Extensions.Meter.IMeter");
            if (meterSymbol == null)
            {
                // nothing to do if this type isn't available
                return Array.Empty<MetricInstrumentClass>();
            }

            var results = new List<MetricInstrumentClass>();
            var metricNames = new HashSet<string>();

            foreach (var classDeclarationGroup in classes.GroupBy(x => x.SyntaxTree))
            {
                SemanticModel? semanticModel = null;
                foreach (var classDeclaration in classDeclarationGroup)
                {
                    // stop if we're asked to
                    _cancellationToken.ThrowIfCancellationRequested();

                    MetricInstrumentClass? metricInstrumentClass = null;
                    string nspace = string.Empty;

                    metricNames.Clear();

                    foreach (var member in classDeclaration.Members)
                    {
                        var method = member as MethodDeclarationSyntax;
                        if (method == null)
                        {
                            // we only care about methods
                            continue;
                        }

                        foreach (var methodAttributeList in member.AttributeLists)
                        {
                            foreach (var methodAttribute in methodAttributeList.Attributes)
                            {
                                semanticModel ??= _compilation.GetSemanticModel(classDeclaration.SyntaxTree);

                                var methodAttributeSymbol = semanticModel.GetSymbolInfo(methodAttribute, _cancellationToken).Symbol as IMethodSymbol;
                                if (methodAttributeSymbol == null)
                                {
                                    // badly formed attribute definition, or not the right attribute
                                    continue;
                                }

                                bool isCounterAttribute = counterAttribute.Equals(methodAttributeSymbol.ContainingType, SymbolEqualityComparer.Default);
                                bool isValueRecorderAttribute = valueRecorderAttribute.Equals(methodAttributeSymbol.ContainingType, SymbolEqualityComparer.Default);

                                if (isCounterAttribute || isValueRecorderAttribute)
                                {
                                    var methodSymbol = semanticModel.GetDeclaredSymbol(method, _cancellationToken);

                                    if (methodSymbol != null)
                                    {
                                        var (staticDim, dynamicDim) = ExtractDimensions(methodAttribute.ArgumentList, semanticModel);
                                        string metricName = semanticModel.GetTypeInfo(method.ReturnType!).Type!.Name;
                                        var metricInstrumentMethod = new MetricInstrumentMethod
                                        {
                                            Name = method.Identifier.ToString(),
                                            MetricName = metricName,
                                            InstrumentType = isCounterAttribute ? InstrumentType.Counter : InstrumentType.ValueRecorder,
                                            StaticDimensionsKeys = GetDimensionsList(staticDim),
                                            DynamicDimensionsKeys = GetDimensionsList(dynamicDim),
                                            IsExtensionMethod = methodSymbol.IsExtensionMethod,
                                            Modifiers = method.Modifiers.ToString(),
                                            InstrumentClassType = semanticModel.GetTypeInfo(method.ReturnType!).Type!.ToDisplayString()
                                        };

                                        bool keepMethod = true;
                                        if (metricInstrumentMethod.Name[0] == '_')
                                        {
                                            // can't have logging method names that start with _ since that can lead to conflicting symbol names
                                            // because the generated symbols start with _
                                            Diag(DiagDescriptors.ErrorInvalidMethodName, method.Identifier.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (semanticModel.GetTypeInfo(method.ReturnType!).Type!.SpecialType != SpecialType.None)
                                        {
                                            // Make sure return type is not from existing known type
                                            Diag(DiagDescriptors.ErrorInvalidMethodReturnType, method.ReturnType.GetLocation(), metricInstrumentMethod.MetricName);
                                            keepMethod = false;
                                        }

                                        if (method.Arity > 0)
                                        {
                                            // we don't currently support generic methods
                                            Diag(DiagDescriptors.ErrorMethodIsGeneric, method.Identifier.GetLocation());
                                            keepMethod = false;
                                        }

                                        bool isStatic = false;
                                        bool isPartial = false;
                                        foreach (var mod in method.Modifiers)
                                        {
                                            switch (mod.Text)
                                            {
                                                case "partial":
                                                    isPartial = true;
                                                    break;

                                                case "static":
                                                    isStatic = true;
                                                    break;
                                            }
                                        }

                                        if (!isPartial)
                                        {
                                            Diag(DiagDescriptors.ErrorNotPartialMethod, method.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (!isStatic)
                                        {
                                            Diag(DiagDescriptors.ErrorNotStaticMethod, method.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (method.Body != null)
                                        {
                                            Diag(DiagDescriptors.ErrorMethodHasBody, method.Body.GetLocation());
                                            keepMethod = false;
                                        }

                                        // ensure Metric name is not empty and starts with a Capital letter.
                                        // ensure there are no duplicate ids.
                                        if (!_regex.IsMatch(metricInstrumentMethod.MetricName))
                                        {
                                            Diag(DiagDescriptors.ErrorInvalidMetricName, methodAttribute.GetLocation(), metricInstrumentMethod.MetricName);
                                            keepMethod = false;
                                        }
                                        else if (metricNames.Contains(metricInstrumentMethod.MetricName!))
                                        {
                                            Diag(DiagDescriptors.ErrorMetricNameReuse, methodAttribute.GetLocation(), metricInstrumentMethod.MetricName);
                                            keepMethod = false;
                                        }
                                        else
                                        {
                                            _ = metricNames.Add(metricInstrumentMethod.MetricName);
                                        }

                                        if (!AreDimensionKeyNamesValid(metricInstrumentMethod))
                                        {
                                            Diag(DiagDescriptors.ErrorInvalidDimensionNames, methodAttribute.GetLocation());
                                            keepMethod = false;
                                        }

                                        bool foundMeter = false;
                                        foreach (var parameter in method.ParameterList.Parameters)
                                        {
                                            var paramName = parameter.Identifier.ToString();
                                            if (string.IsNullOrWhiteSpace(paramName))
                                            {
                                                // semantic problem, just bail quietly
                                                keepMethod = false;
                                                break;
                                            }

                                            var declSymbol = semanticModel.GetDeclaredSymbol(parameter);
                                            var paramSymbol = declSymbol!.Type;
                                            if (paramSymbol is IErrorTypeSymbol)
                                            {
                                                // semantic problem, just bail quietly
                                                keepMethod = false;
                                                break;
                                            }

                                            if (metricInstrumentMethod.DynamicDimensionsKeys.Contains(paramName))
                                            {
                                                Diag(DiagDescriptors.ErrorConflictingDimension, parameter.Identifier.GetLocation());
                                                keepMethod = false;
                                            }

                                            var declaredType = semanticModel.GetDeclaredSymbol(parameter);
                                            var typeName = declaredType!.ToDisplayString();

                                            var meterParameter = new MetricInstrumentParameter
                                            {
                                                Name = paramName,
                                                Type = typeName,
                                                IsMeter = !foundMeter && IsBaseOrIdentity(paramSymbol!, meterSymbol),
                                            };

                                            foundMeter |= meterParameter.IsMeter;

                                            if (meterParameter.Name[0] == '_')
                                            {
                                                // can't have method parameter names that start with _ since that can lead to conflicting symbol names
                                                // because all generated symbols start with _
                                                Diag(DiagDescriptors.ErrorInvalidParameterName, parameter.Identifier.GetLocation());
                                            }

                                            metricInstrumentMethod.AllParameters.Add(meterParameter);

                                            if (meterParameter.IsRegular)
                                            {
                                                metricInstrumentMethod.StaticDimensionParameters.Add(meterParameter);
                                            }

                                            if (keepMethod)
                                            {
                                                if (!foundMeter)
                                                {
                                                    Diag(DiagDescriptors.ErrorMissingMeter, method.GetLocation());
                                                    keepMethod = false;
                                                }
                                            }
                                        }

                                        if (metricInstrumentMethod.StaticDimensionParameters.Count != metricInstrumentMethod.StaticDimensionsKeys.Count)
                                        {
                                            Diag(DiagDescriptors.ErrorMismatchingStaticDimensionsCount, method.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (metricInstrumentClass == null)
                                        {
                                            // determine the namespace the class is declared in, if any
                                            var ns = classDeclaration.Parent as NamespaceDeclarationSyntax;
                                            if (ns == null)
                                            {
                                                if (classDeclaration.Parent is not CompilationUnitSyntax)
                                                {
                                                    // since this generator doesn't know how to generate a nested type...
                                                    Diag(DiagDescriptors.ErrorNestedType, classDeclaration.Identifier.GetLocation());
                                                    keepMethod = false;
                                                }
                                            }
                                            else
                                            {
                                                nspace = ns.Name.ToString();
                                                while (true)
                                                {
                                                    ns = ns.Parent as NamespaceDeclarationSyntax;
                                                    if (ns == null)
                                                    {
                                                        break;
                                                    }

                                                    nspace = $"{ns.Name}.{nspace}";
                                                }
                                            }
                                        }

                                        if (keepMethod)
                                        {
                                            metricInstrumentClass ??= new MetricInstrumentClass
                                            {
                                                Namespace = nspace,
                                                Name = classDeclaration.Identifier.ToString() + classDeclaration.TypeParameterList,
                                                Constraints = classDeclaration.ConstraintClauses.ToString(),
                                            };

                                            metricInstrumentClass.Methods.Add(metricInstrumentMethod);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (metricInstrumentClass != null)
                    {
                        results.Add(metricInstrumentClass);
                    }
                }
            }

            return results;
        }

        private static bool AreDimensionKeyNamesValid(MetricInstrumentMethod metricInstrumentMethod)
        {
            foreach (var staticDim in metricInstrumentMethod.StaticDimensionsKeys)
            {
                if (!_regexDimensionNames.IsMatch(staticDim))
                {
                    return false;
                }
            }

            foreach (var dynDim in metricInstrumentMethod.DynamicDimensionsKeys)
            {
                if (!_regexDimensionNames.IsMatch(dynDim))
                {
                    return false;
                }
            }

            return true;
        }

        private static HashSet<string> GetDimensionsList(string dimensionListString)
        {
            if (string.IsNullOrWhiteSpace(dimensionListString))
            {
                return new ();
            }

            var dimensionTokens = dimensionListString.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            return new HashSet<string>(dimensionTokens);
        }

        private bool IsBaseOrIdentity(ITypeSymbol source, ITypeSymbol dest)
        {
            var conversion = _compilation.ClassifyConversion(source, dest);
            return (conversion.IsReference && conversion.IsImplicit) || conversion.IsIdentity;
        }

        private (string, string) ExtractDimensions(AttributeArgumentListSyntax? args, SemanticModel sm)
        {
            string staticDim = string.Empty;
            string dynamicDim = string.Empty;

            if (args != null)
            {
                foreach (var arg in args.Arguments)
                {
                    if (arg.NameEquals != null)
                    {
                        switch (arg.NameEquals.Name.ToString())
                        {
                            case "StaticDimensions":
                                staticDim = (string)(sm.GetConstantValue(arg.Expression, _cancellationToken).Value ?? string.Empty);
                                break;
                            case "DynamicDimensions":
                                dynamicDim = (string)(sm.GetConstantValue(arg.Expression, _cancellationToken).Value ?? string.Empty);
                                break;
                        }
                    }
                }
            }

            return (staticDim, dynamicDim);
        }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
        private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
        {
            _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
        }
    }

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1402 // File may only contain a single type
    internal class MetricInstrumentClass
    {
        public readonly List<MetricInstrumentMethod> Methods = new ();
        public string Namespace = string.Empty;
        public string Name = string.Empty;
        public string Constraints = string.Empty;
    }

    internal class MetricInstrumentMethod
    {
        public readonly List<MetricInstrumentParameter> AllParameters = new ();
        public readonly List<MetricInstrumentParameter> StaticDimensionParameters = new ();
        public HashSet<string> StaticDimensionsKeys = new ();
        public HashSet<string> DynamicDimensionsKeys = new ();
        public string? Name;
        public string? MetricName;
        public bool IsExtensionMethod;
        public string Modifiers = string.Empty;
        public string InstrumentClassType = string.Empty;
        public InstrumentType InstrumentType;
    }

    internal class MetricInstrumentParameter
    {
        public string Name = string.Empty;
        public string Type = string.Empty;
        public bool IsMeter;
        public bool IsRegular => !IsMeter;
    }

    internal enum InstrumentType
    {
        Counter = 0,
        ValueRecorder = 1
    }
}
