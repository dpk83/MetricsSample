using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricUtilities;

namespace MetricsBench
{
    public static partial class Metric
    {
        [Int64CounterMetric("counter5d")]
        public static partial Counter5D Create5dCounter(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric("counter10d")]
        public static partial Counter10D Create10dCounter(IMeter meter, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10);
    }
}
