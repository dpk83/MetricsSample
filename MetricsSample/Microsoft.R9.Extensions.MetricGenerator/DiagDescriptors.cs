using Microsoft.CodeAnalysis;

namespace Microsoft.R9.Extensions.MetricGenerator
{
    internal static class MetricDiagDescriptors
    {
        public static DiagnosticDescriptor ErrorInvalidMethodName { get; } = new(
            id: "R9MG0000",
            title: MetricResources.ErrorInvalidMethodNameTitle,
            messageFormat: MetricResources.ErrorInvalidMethodNameMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidParameterName { get; } = new(
            id: "R9MG0001",
            title: MetricResources.ErrorInvalidParameterNameTitle,
            messageFormat: MetricResources.ErrorInvalidParameterNameMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNestedType { get; } = new(
            id: "R9MG0002",
            title: MetricResources.ErrorNestedTypeTitle,
            messageFormat: MetricResources.ErrorNestedTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingRequiredType { get; } = new(
            id: "R9MG0003",
            title: MetricResources.ErrorMissingRequiredTypeTitle,
            messageFormat: MetricResources.ErrorMissingRequiredTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMetricNameReuse { get; } = new(
            id: "R9MG0004",
            title: MetricResources.ErrorMetricNameReuseTitle,
            messageFormat: MetricResources.ErrorMetricNameReuseMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidMethodReturnType { get; } = new(
            id: "R9MG0005",
            title: MetricResources.ErrorInvalidMethodReturnTypeTitle,
            messageFormat: MetricResources.ErrorInvalidMethodReturnTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingMeter { get; } = new(
            id: "R9MG0006",
            title: MetricResources.ErrorMissingMeterTitle,
            messageFormat: MetricResources.ErrorMissingMeterMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNotPartialMethod { get; } = new(
            id: "R9MG0007",
            title: MetricResources.ErrorNotPartialMethodTitle,
            messageFormat: MetricResources.ErrorNotPartialMethodMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodIsGeneric { get; } = new(
            id: "R9MG0008",
            title: MetricResources.ErrorMethodIsGenericTitle,
            messageFormat: MetricResources.ErrorMethodIsGenericMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodHasBody { get; } = new(
            id: "R9MG0009",
            title: MetricResources.ErrorMethodHasBodyTitle,
            messageFormat: MetricResources.ErrorMethodHasBodyMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingStaticDimension { get; } = new(
            id: "R9MG0010",
            title: MetricResources.ErrorMissingStaticDimensionParameterTitle,
            messageFormat: MetricResources.ErrorMissingStaticDimensionParameterMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingStaticDimensionParameter { get; } = new(
            id: "R9MG0011",
            title: MetricResources.ErrorMissingStaticDimensionTitle,
            messageFormat: MetricResources.ErrorMissingStaticDimensionMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNotStaticMethod { get; } = new(
            id: "R9MG0012",
            title: MetricResources.ErrorNotStaticMethodTitle,
            messageFormat: MetricResources.ErrorNotStaticMethodMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
