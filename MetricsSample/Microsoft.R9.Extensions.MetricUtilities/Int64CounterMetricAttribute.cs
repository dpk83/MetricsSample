using System;

namespace Microsoft.R9.Extensions.MetricUtilities
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Int64CounterMetricAttribute : Attribute
    {
        public Int64CounterMetricAttribute(string metricName)
        {
            MetricName = metricName;
        }

        /// <summary>
        /// Unique name of the metric.
        /// </summary>
        public string MetricName { get; }
    }
}
