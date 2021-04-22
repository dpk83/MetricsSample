// © Microsoft Corporation. All rights reserved.

using Microsoft.CodeAnalysis;

namespace Microsoft.R9.Generators.Metric
{
    internal static class DiagDescriptors
    {
        private const string _category = "Metric";

        public static DiagnosticDescriptor ErrorInvalidMethodName { get; } = new (
            id: "R9G050",
            title: Resources.ErrorInvalidMethodNameTitle,
            messageFormat: Resources.ErrorInvalidMethodNameMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g050",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidParameterName { get; } = new (
            id: "R9G051",
            title: Resources.ErrorInvalidParameterNameTitle,
            messageFormat: Resources.ErrorInvalidParameterNameMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g051",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNestedType { get; } = new (
            id: "R9G052",
            title: Resources.ErrorNestedTypeTitle,
            messageFormat: Resources.ErrorNestedTypeMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g052",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidMetricName { get; } = new (
            id: "R9G053",
            title: Resources.ErrorInvalidMetricNameTitle,
            messageFormat: Resources.ErrorInvalidMetricNameMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g053",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMetricNameReuse { get; } = new (
            id: "R9G054",
            title: Resources.ErrorMetricNameReuseTitle,
            messageFormat: Resources.ErrorMetricNameReuseMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g054",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidMethodReturnType { get; } = new (
            id: "R9G055",
            title: Resources.ErrorInvalidMethodReturnTypeTitle,
            messageFormat: Resources.ErrorInvalidMethodReturnTypeMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g055",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMissingMeter { get; } = new (
            id: "R9G056",
            title: Resources.ErrorMissingMeterTitle,
            messageFormat: Resources.ErrorMissingMeterMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g056",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNotPartialMethod { get; } = new (
            id: "R9G057",
            title: Resources.ErrorNotPartialMethodTitle,
            messageFormat: Resources.ErrorNotPartialMethodMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g057",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodIsGeneric { get; } = new (
            id: "R9G058",
            title: Resources.ErrorMethodIsGenericTitle,
            messageFormat: Resources.ErrorMethodIsGenericMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g058",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMethodHasBody { get; } = new (
            id: "R9G059",
            title: Resources.ErrorMethodHasBodyTitle,
            messageFormat: Resources.ErrorMethodHasBodyMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g059",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorInvalidDimensionNames { get; } = new (
            id: "R9G060",
            title: Resources.ErrorInvalidDimensionNamesMessage,
            messageFormat: Resources.ErrorInvalidDimensionNamesTitle,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g060",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorConflictingDimension { get; } = new (
            id: "R9G061",
            title: Resources.ErrorConflictingDimensionMessage,
            messageFormat: Resources.ErrorConflictingDimensionTitle,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g061",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorNotStaticMethod { get; } = new (
            id: "R9G062",
            title: Resources.ErrorNotStaticMethodTitle,
            messageFormat: Resources.ErrorNotStaticMethodMessage,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g062",
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMismatchingStaticDimensionsCount { get; } = new (
            id: "R9G063",
            title: Resources.ErrorMismatchingStaticDimensionsCountMessage,
            messageFormat: Resources.ErrorMismatchingStaticDimensionsCountTitle,
            category: _category,
            DiagnosticSeverity.Error,
            helpLinkUri: "https://eng.ms/docs/experiences-devices/r9-sdk/docs/code-analysis-and-generation/errors/r9g063",
            isEnabledByDefault: true);
    }
}
