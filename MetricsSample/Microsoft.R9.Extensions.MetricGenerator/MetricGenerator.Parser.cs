using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.R9.Extensions.MetricGenerator
{
    public partial class MetricGenerator
    {
        internal class Parser
        {
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
                const string CounterAttribute = "Microsoft.R9.Extensions.MetricUtilities.Int64CounterMetricAttribute";

                var counterAttribute = _compilation.GetTypeByMetadataName(CounterAttribute);
                if (counterAttribute is null)
                {
                    // nothing to do if this type isn't available
                    return Array.Empty<MetricInstrumentClass>();
                }

                var meterSymbol = _compilation.GetTypeByMetadataName("Microsoft.R9.Extensions.Meter.IMeter");
                if (meterSymbol == null)
                {
                    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "Microsoft.R9.Extensions.Meter.IMeter");
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
                                    if (methodAttributeSymbol == null ||
                                        !counterAttribute.Equals(methodAttributeSymbol.ContainingType, SymbolEqualityComparer.Default))
                                    {
                                        // badly formed attribute definition, or not the right attribute
                                        continue;
                                    }

                                    var (metricName, staticDim, dynamicDim) = ExtractMetricName(methodAttribute.ArgumentList!, semanticModel);
                                    var methodSymbol = semanticModel.GetDeclaredSymbol(method, _cancellationToken);

                                    if (methodSymbol != null)
                                    {
                                        var metricInstrumentMethod = new MetricInstrumentMethod
                                        {
                                            Name = method.Identifier.ToString(),
                                            MetricName = metricName,
                                            StaticDimensions = GetDimensionsList(staticDim),
                                            DynamicDimensions = GetDimensionsList(dynamicDim),
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

                                        if (semanticModel.GetTypeInfo(method.ReturnType!).Type!.SpecialType != SpecialType.None) //!= SpecialType.System_Void)
                                        {
                                            // Make sure return type is not from existing none type
                                            Diag(DiagDescriptors.ErrorInvalidMethodReturnType, method.ReturnType.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (method.Arity > 0)
                                        {
                                            // we don't currently support generic methods
                                            Diag(DiagDescriptors.ErrorMethodIsGeneric, method.Identifier.GetLocation());
                                            keepMethod = false;
                                        }

                                        // bool isStatic = false;
                                        bool isPartial = false;
                                        foreach (var mod in method.Modifiers)
                                        {
                                            switch (mod.Text)
                                            {
                                                case "partial":
                                                    isPartial = true;
                                                    break;

                                                case "static":
                                                    //isStatic = true;
                                                    break;
                                            }
                                        }

                                        if (!isPartial)
                                        {
                                            Diag(DiagDescriptors.ErrorNotPartialMethod, method.GetLocation());
                                            keepMethod = false;
                                        }

                                        if (method.Body != null)
                                        {
                                            Diag(DiagDescriptors.ErrorMethodHasBody, method.Body.GetLocation());
                                            keepMethod = false;
                                        }

#pragma warning disable CS8604 // Possible null reference argument.
                                        // ensure there are no duplicate ids.
                                        if (string.IsNullOrWhiteSpace(metricInstrumentMethod.MetricName) ||
                                            metricNames.Contains(metricInstrumentMethod.MetricName!))
                                        {
                                            Diag(DiagDescriptors.ErrorMetricNameReuse, methodAttribute.GetLocation(), metricInstrumentMethod.MetricName);
                                        }
                                        else
                                        {
                                            _ = metricNames.Add(metricInstrumentMethod.MetricName);
                                        }
#pragma warning restore CS8604 // Possible null reference argument.

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
                                                metricInstrumentMethod.RegularParameters.Add(meterParameter);
                                            }

                                            if (keepMethod)
                                            {
                                                if (!foundMeter)
                                                {
                                                    Diag(DiagDescriptors.ErrorMissingMeter, method.GetLocation());
                                                    keepMethod = false;
                                                }
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
                                        }

                                        foreach(var param in metricInstrumentMethod.RegularParameters)
                                        {
                                            if (!metricInstrumentMethod.StaticDimensions.Contains(param.Name))
                                            {
                                                // Report error, all dimenions passed to the method should be static dimensions
                                                // and should be provided as StaticDimensions in the attribute
                                                Diag(DiagDescriptors.ErrorMissingStaticDimension, method.GetLocation(), param.Name);
                                                keepMethod = false;
                                            }
                                        }

                                        if (metricInstrumentMethod.RegularParameters.Count != metricInstrumentMethod.StaticDimensions.Count)
                                        {
                                            // report error, all dimensions passed in attribute as static dimensions should be 
                                            // passed as string parameters to the method
                                            Diag(DiagDescriptors.ErrorMissingStaticDimension, method.GetLocation());
                                            keepMethod = false;
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
                                            keepMethod = false;
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


            private bool IsBaseOrIdentity(ITypeSymbol source, ITypeSymbol dest)
            {
                var conversion = _compilation.ClassifyConversion(source, dest);
                return conversion.IsIdentity || (conversion.IsReference && conversion.IsImplicit);
            }

            private (string?, string, string) ExtractMetricName(AttributeArgumentListSyntax args, SemanticModel sm)
            {
                string? metricName = null;
                string staticDim = string.Empty;
                string dynamicDim = string.Empty;
                int index = 0;
                foreach (var arg in args.Arguments)
                {
                    if (index == 0)
                    {
                        metricName = (string)sm.GetConstantValue(arg.Expression, _cancellationToken).Value!;
                    }
                    else if (index == 1)
                    {
                        staticDim = (string)sm.GetConstantValue(arg.Expression, _cancellationToken).Value!;
                    }
                    else if (index == 2)
                    {
                        dynamicDim = (string)sm.GetConstantValue(arg.Expression, _cancellationToken).Value!;
                    }
                    index++;
                }

                return (metricName, staticDim, dynamicDim);
            }

            private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
            {
                _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
            }

            private List<string> GetDimensionsList(string dimensionListString)
            {
                if (string.IsNullOrWhiteSpace(dimensionListString))
                {
                    return new();
                }

                var dimensionTokens = Regex.Split(dimensionListString, ",");
                return new List<string>(dimensionTokens);
            }
        }

        internal class MetricInstrumentClass
        {
            public readonly List<MetricInstrumentMethod> Methods = new();
            public string Namespace = string.Empty;
            public string Name = string.Empty;
            public string Constraints = string.Empty;
        }

        internal class MetricInstrumentMethod
        {
            public readonly List<MetricInstrumentParameter> AllParameters = new();
            public readonly List<MetricInstrumentParameter> RegularParameters = new();
            public List<string> StaticDimensions = new();
            public List<string> DynamicDimensions = new();
            public string? Name;
            public string? MetricName;
            public bool IsExtensionMethod;
            public string Modifiers = string.Empty;
            public string InstrumentClassType = string.Empty;
        }

        internal class MetricInstrumentParameter
        {
            public string Name = string.Empty;
            public string Type = string.Empty;
            public bool IsMeter = false;
            public bool IsRegular => !IsMeter;
        }
    }
}
