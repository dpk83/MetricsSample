// © Microsoft Corporation. All rights reserved.

using System;

namespace Microsoft.R9.Extensions.Meter
{
    /// <summary>
    /// Provides information to guide the production of a strongly-typed Int64 counter type metric method.
    /// </summary>
    /// <remarks>
    /// This class is used to guide the production of strongly-typed Int64 counter type metric method.
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
    ///     [Int64CounterMetric(StaticDimensions = "EnvironmentName,Host", DynamicDimensions= "RequestName,RequestStatusCode")]
    ///     static partial RequestCounter CreateRequestCounter(IMeter meter, string env, string hostName);
    /// }.
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Int64CounterMetricAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a comma-separated list of dimension keys that are dynamic i.e., their values change during counter lifetime.
        /// </summary>
        public string? Dimensions { get; set; }
    }
}
