using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricUtilities;

namespace MetricsBench
{
    public static partial class Metric
    {
        [Int64CounterMetric("Counter5D")]
        public static partial ICounterMetric<long> Create5dCounter(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric("Counter10D")]
        public static partial ICounterMetric<long> Create10dCounter(IMeter meter, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10);
    }
}
