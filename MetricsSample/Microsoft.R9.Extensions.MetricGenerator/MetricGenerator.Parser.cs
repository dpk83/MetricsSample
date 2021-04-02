using System;
using System.Collections.Generic;
using System.Linq;
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

            public IReadOnlyList<CounterClass> GetCounterClasses(IEnumerable<ClassDeclarationSyntax> classes)
            {
                const string CounterAttribute = "Microsoft.R9.Extensions.MetricUtilities.Int64CounterMetricAttribute";

                var counterAttribute = _compilation.GetTypeByMetadataName(CounterAttribute);
                if (counterAttribute is null)
                {
                    // nothing to do if this type isn't available
                    return Array.Empty<CounterClass>();
                }

                //var meterSymbol = _compilation.GetTypeByMetadataName("Microsoft.R9.Extensions.Meter.IMeter");
                //if (meterSymbol == null)
                //{
                //    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "System.Exception");
                //    // nothing to do if this type isn't available
                //    return Array.Empty<CounterClass>();
                //}

                var exceptionSymbol = _compilation.GetTypeByMetadataName("System.Exception");
                if (exceptionSymbol == null)
                {
                    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "System.Exception");
                    return Array.Empty<CounterClass>();
                }

                var enumerableSymbol = _compilation.GetTypeByMetadataName("System.Collections.IEnumerable");
                if (enumerableSymbol == null)
                {
                    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "System.Collections.IEnumerable");
                    return Array.Empty<CounterClass>();
                }

                var stringSymbol = _compilation.GetTypeByMetadataName("System.String");
                if (stringSymbol == null)
                {
                    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "System.String");
                    return Array.Empty<CounterClass>();
                }

                var dateTimeSymbol = _compilation.GetTypeByMetadataName("System.DateTime");
                if (dateTimeSymbol == null)
                {
                    Diag(DiagDescriptors.ErrorMissingRequiredType, null, "System.DateTime");
                    return Array.Empty<CounterClass>();
                }

                var results = new List<CounterClass>();
                var ids = new HashSet<int>();


                foreach (var group in classes.GroupBy(x => x.SyntaxTree))
                {
                    SemanticModel? sm = null;
                    foreach (var classDef in group)
                    {
                        // stop if we're asked to
                        _cancellationToken.ThrowIfCancellationRequested();

                        CounterClass? counterClass = null;
                        string nspace = string.Empty;

                        ids.Clear();

                        foreach (var member in classDef.Members)
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
                                    sm ??= _compilation.GetSemanticModel(classDef.SyntaxTree);

                                    var methodAttributeSymbol = sm.GetSymbolInfo(methodAttribute, _cancellationToken).Symbol as IMethodSymbol;
                                    if (methodAttributeSymbol == null ||
                                        !counterAttribute.Equals(methodAttributeSymbol.ContainingType, SymbolEqualityComparer.Default))
                                    {
                                        // badly formed attribute definition, or not the right attribute
                                        continue;
                                    }

                                    var (eventId, eventName) = ExtractAttributeValues(methodAttribute.ArgumentList!, sm);

                                    var methodSymbol = sm.GetDeclaredSymbol(method, _cancellationToken);

                                    if (methodSymbol != null)
                                    {
                                        var counterMethod = new CounterMethod
                                        {
                                            Name = method.Identifier.ToString(),
                                            EventId = eventId,
                                            EventName = eventName,
                                            IsExtensionMethod = methodSymbol.IsExtensionMethod,
                                            Modifiers = method.Modifiers.ToString(),
                                            Type = sm.GetTypeInfo(method.ReturnType!).Type!.ToDisplayString()
                                        };

                                        bool keepMethod = true;
                                        if (counterMethod.Name[0] == '_')
                                        {
                                            // can't have logging method names that start with _ since that can lead to conflicting symbol names
                                            // because the generated symbols start with _
                                            Diag(DiagDescriptors.ErrorInvalidMethodName, method.Identifier.GetLocation());
                                        }

                                        if (sm.GetTypeInfo(method.ReturnType!).Type!.SpecialType != SpecialType.None) //!= SpecialType.System_Void)
                                        {
                                            // Make sure return type is an object type
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

                                        // ensure there are no duplicate ids.
                                        if (ids.Contains(counterMethod.EventId))
                                        {
                                            Diag(DiagDescriptors.ErrorEventIdReuse, methodAttribute.GetLocation(), counterMethod.EventId);
                                        }
                                        else
                                        {
                                            _ = ids.Add(counterMethod.EventId);
                                        }

                                        //bool foundMeter = false;
                                        foreach (var p in method.ParameterList.Parameters)
                                        {
                                            var paramName = p.Identifier.ToString();
                                            if (string.IsNullOrWhiteSpace(paramName))
                                            {
                                                // semantic problem, just bail quietly
                                                keepMethod = false;
                                                break;
                                            }

                                            var declSymbol = sm.GetDeclaredSymbol(p);
                                            var paramSymbol = declSymbol!.Type;
                                            if (paramSymbol is IErrorTypeSymbol)
                                            {
                                                // semantic problem, just bail quietly
                                                keepMethod = false;
                                                break;
                                            }

                                            var declaredType = sm.GetDeclaredSymbol(p);
                                            var typeName = declaredType!.ToDisplayString();

                                            var meterParameter = new MeterParameter
                                            {
                                                Name = paramName,
                                                Type = typeName,
                                                // IsMeter = !foundMeter && IsBaseOrIdentity(paramSymbol!, meterSymbol),
                                                IsEnumerable = IsBaseOrIdentity(paramSymbol!, enumerableSymbol) && !IsBaseOrIdentity(paramSymbol!, stringSymbol),
                                            };

                                            // foundMeter |= meterParameter.IsMeter;

                                            if (IsBaseOrIdentity(paramSymbol!, dateTimeSymbol))
                                            {
                                                Diag(DiagDescriptors.PassingDateTime, p.Identifier.GetLocation());
                                            }

                                            if (meterParameter.Name[0] == '_')
                                            {
                                                // can't have logging method parameter names that start with _ since that can lead to conflicting symbol names
                                                // because all generated symbols start with _
                                                Diag(DiagDescriptors.ErrorInvalidParameterName, p.Identifier.GetLocation());
                                            }

                                            counterMethod.AllParameters.Add(meterParameter);

                                            if (meterParameter.IsRegular)
                                            {
                                                counterMethod.RegularParameters.Add(meterParameter);
                                            }

                                            //if (keepMethod)
                                            //{
                                            //    if (isStatic && !foundMeter)
                                            //    {
                                            //        Diag(DiagDescriptors.ErrorMissingLogger, method.GetLocation());
                                            //        keepMethod = false;
                                            //    }
                                            //    else if (!isStatic && foundMeter)
                                            //    {
                                            //        Diag(DiagDescriptors.ErrorNotStaticMethod, method.GetLocation());
                                            //    }
                                            //}

                                            if (counterClass == null)
                                            {
                                                // determine the namespace the class is declared in, if any
                                                var ns = classDef.Parent as NamespaceDeclarationSyntax;
                                                if (ns == null)
                                                {
                                                    if (classDef.Parent is not CompilationUnitSyntax)
                                                    {
                                                        // since this generator doesn't know how to generate a nested type...
                                                        Diag(DiagDescriptors.ErrorNestedType, classDef.Identifier.GetLocation());
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
                                                counterClass ??= new CounterClass
                                                {
                                                    Namespace = nspace,
                                                    Name = classDef.Identifier.ToString() + classDef.TypeParameterList,
                                                    Constraints = classDef.ConstraintClauses.ToString(),
                                                };

                                                counterClass.Methods.Add(counterMethod);
                                                keepMethod = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (counterClass != null)
                        {
                            results.Add(counterClass);
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

            private (int eventId, string? eventName) ExtractAttributeValues(AttributeArgumentListSyntax args, SemanticModel sm)
            {
                int eventId = 0;
                string? eventName = null;
                int numPositional = 0;

                foreach (var arg in args.Arguments)
                {
                    if (arg.NameEquals != null)
                    {
                        switch (arg.NameEquals.Name.ToString())
                        {
                            case "EventName":
                                eventName = sm.GetConstantValue(arg.Expression, _cancellationToken).ToString();
                                break;
                        }
                    }
                    else if (arg.NameColon != null)
                    {
                        switch (arg.NameColon.Name.ToString())
                        {
                            case "eventId":
                                eventId = (int)sm.GetConstantValue(arg.Expression, _cancellationToken).Value!;
                                break;
                        }
                    }
                    else
                    {
                        switch (numPositional)
                        {
                            // event id
                            case 0:
                                eventId = (int)sm.GetConstantValue(arg.Expression, _cancellationToken).Value!;
                                break;
                        }

                        numPositional++;
                    }
                }

                return (eventId, eventName);
            }

            private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
            {
                _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
            }
        }

        internal class CounterClass
        {
            public readonly List<CounterMethod> Methods = new();
            public string Namespace = string.Empty;
            public string Name = string.Empty;
            public string Constraints = string.Empty;
        }

        internal class CounterMethod
        {
            public readonly List<MeterParameter> AllParameters = new();
            public readonly List<MeterParameter> RegularParameters = new();
            public string? Name;
            public int EventId;
            public string? EventName;
            public bool IsExtensionMethod;
            public string Modifiers = string.Empty;
            public string Type = string.Empty;
        }

        internal class MeterParameter
        {
            public string Name = string.Empty;
            public string Type = string.Empty;
            public bool IsMeter = false;
            public bool IsEnumerable;
            public bool IsRegular => !IsMeter;
        }
    }
}
