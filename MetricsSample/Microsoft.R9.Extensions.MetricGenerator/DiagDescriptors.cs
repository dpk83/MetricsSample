using Microsoft.CodeAnalysis;

namespace Microsoft.R9.Extensions.MetricGenerator
{
    internal static class DiagDescriptors
    {
        public static DiagnosticDescriptor ErrorInvalidMethodName { get; } = new(
            id: "MG0000",
            title: Resources.ErrorInvalidMethodNameTitle,
            messageFormat: Resources.ErrorInvalidMethodNameMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidParameterName { get; } = new(
            id: "MG0001",
            title: Resources.ErrorInvalidParameterNameTitle,
            messageFormat: Resources.ErrorInvalidParameterNameMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNestedType { get; } = new(
            id: "MG0002",
            title: Resources.ErrorNestedTypeTitle,
            messageFormat: Resources.ErrorNestedTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingRequiredType { get; } = new(
            id: "MG0003",
            title: Resources.ErrorMissingRequiredTypeTitle,
            messageFormat: Resources.ErrorMissingRequiredTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMetricNameReuse { get; } = new(
            id: "MG0004",
            title: Resources.ErrorMetricNameReuseTitle,
            messageFormat: Resources.ErrorMetricNameReuseMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidMethodReturnType { get; } = new(
            id: "MG0005",
            title: Resources.ErrorInvalidMethodReturnTypeTitle,
            messageFormat: Resources.ErrorInvalidMethodReturnTypeMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingMeter { get; } = new(
            id: "MG0006",
            title: Resources.ErrorMissingMeterTitle,
            messageFormat: Resources.ErrorMissingMeterMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNotPartialMethod { get; } = new(
            id: "MG0007",
            title: Resources.ErrorNotPartialMethodTitle,
            messageFormat: Resources.ErrorNotPartialMethodMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodIsGeneric { get; } = new(
            id: "MG0008",
            title: Resources.ErrorMethodIsGenericTitle,
            messageFormat: Resources.ErrorMethodIsGenericMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodHasBody { get; } = new(
            id: "MG0009",
            title: Resources.ErrorMethodHasBodyTitle,
            messageFormat: Resources.ErrorMethodHasBodyMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingStaticDimension { get; } = new(
            id: "MG0010",
            title: Resources.ErrorMissingStaticDimensionParameterTitle,
            messageFormat: Resources.ErrorMissingStaticDimensionParameterMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingStaticDimensionParameter { get; } = new(
            id: "MG0011",
            title: Resources.ErrorMissingStaticDimensionTitle,
            messageFormat: Resources.ErrorMissingStaticDimensionMessage,
            category: "MetricGenerator",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
