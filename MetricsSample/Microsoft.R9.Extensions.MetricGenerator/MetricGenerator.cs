using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.R9.Extensions.MetricGenerator
{
    [Generator]
    public partial class MetricGenerator : ISourceGenerator
    {
        [ExcludeFromCodeCoverage]
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(SyntaxReceiver.Create);
        }

        [ExcludeFromCodeCoverage]
        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = context.SyntaxReceiver as SyntaxReceiver;
            if (receiver == null || receiver.ClassDeclarations.Count == 0)
            {
                // nothing to do yet
                return;
            }

            var parser = new Parser(context.Compilation, context.ReportDiagnostic, context.CancellationToken);
            var emitter = new Emitter();

            var counterClasses = parser.GetCounterClasses(receiver.ClassDeclarations);
            // var result = emitter.Emit(counterClasses, context.CancellationToken);
            // context.AddSource(nameof(MetricGenerator), SourceText.From(result, Encoding.UTF8));

            //var meterInterface = emitter.EmitMeterInterface(counterClasses, context.CancellationToken);
            //context.AddSource("GeneratedMeterInterface", SourceText.From(meterInterface, Encoding.UTF8));

            var genevaMeter = emitter.EmitGenevaMeter(counterClasses, context.CancellationToken);
            context.AddSource("GeneratedGenevaMeter", SourceText.From(genevaMeter, Encoding.UTF8));

            var metricInstruments = emitter.EmitMetricInstruments(counterClasses, context.CancellationToken);
            context.AddSource(nameof(MetricGenerator), SourceText.From(metricInstruments, Encoding.UTF8));
        }

        [ExcludeFromCodeCoverage]
        private sealed class SyntaxReceiver : ISyntaxReceiver
        {
            internal static ISyntaxReceiver Create()
            {
                return new SyntaxReceiver();
            }

            public List<ClassDeclarationSyntax> ClassDeclarations { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax classSyntax)
                {
                    ClassDeclarations.Add(classSyntax);
                }
            }
        }
    }
}
