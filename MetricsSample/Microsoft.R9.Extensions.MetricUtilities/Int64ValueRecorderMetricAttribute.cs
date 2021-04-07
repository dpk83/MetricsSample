// © Microsoft Corporation. All rights reserved.

using System;

namespace Microsoft.R9.Extensions.MetricUtilities
{
    /// <summary>
    /// Provides information to guide the production of a strongly-typed Int64 value recorder type metric method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Int64ValueRecorderMetricAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Int64ValueRecorderMetricAttribute"/> class.
        /// This class is used to guide the production of strongly-typed Int64 value recorder type metric method.
        /// </summary>
        /// <param name="metricName">Name of the metric. Strongly-typed method and classes will be created using this name.</param>
        /// <param name="staticDimensions">A comma separated list of dimensions whose values will not change during metric lifetime.
        /// environment and serviceName are good example of such dimensions.</param>
        /// <param name="dynamicDimensions">A comma separated list of dimensions whose values will change during metric lifetime.
        /// requestName and tenantId are a good example of dynamic dimensions.</param>
        /// <remarks>
        /// The method this attribute is applied to:
        ///    - Must be a partial method.
        ///    - Must return <c>metricName</c> as the type. A class with name provided as metricName field would be generated.
        ///    - Must not be generic.
        ///    - Must have <see cref="IMeter"/> as first parameter.
        ///    - Must have all the keys provided in staticDimension as string type parameters.
        /// </remarks>
        /// <example>
        /// static partial class Metric
        /// {
        ///     [Int64ValueRecorderMetricAttribute("RequestValueRecorder", "env,hostName", "requestName")]
        ///     static partial RequestValueRecorder CreateRequestValueRecorder(IMeter meter, string env, string hostName);
        /// }.
        /// </example>
        public Int64ValueRecorderMetricAttribute(string metricName, string? staticDimensions = null, string? dynamicDimensions = null)
        {
            MetricName = metricName;
            StaticDimensions = staticDimensions;
            DynamicDimensions = dynamicDimensions;
        }

        /// <summary>
        /// Gets the name of the metric.
        /// </summary>
        public string MetricName { get; }

        /// <summary>
        /// Gets the string containing comma separated list of static dimensions.
        /// </summary>
        public string? StaticDimensions { get; }

        /// <summary>
        /// Gets the string containing comma separated list of dynamic dimensions.
        /// </summary>
        public string? DynamicDimensions { get; }
    }
}
