using System;

namespace Microsoft.R9.Extensions.MetricUtilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Int64CounterMetricAttribute : Attribute
    {
        public Int64CounterMetricAttribute(string metricName, string? staticDimensions = null, string? dynamicDimensions = null)
        {
            MetricName = metricName;
            StaticDimensions = staticDimensions;
            DynamicDimensions = staticDimensions;
        }

        /// <summary>
        /// Unique name of the metric.
        /// </summary>
        public string MetricName { get; }

        public string? StaticDimensions { get; }

        public string? DynamicDimensions { get; }
    }
}
