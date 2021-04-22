
using Microsoft.R9.Extensions.Meter;

namespace MetricsBench
{
    public static partial class Metric
    {
        [Int64CounterMetric(StaticDimensions ="k1,k2,k3,k4,k5")]
        public static partial Counter5D CreateCounter5D(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric(StaticDimensions = "k1,k2,k3,k4,k5,k6,k7,k8,k9,k10")]
        public static partial Counter10D CreateCounter10D(IMeter meter, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10);

        [Int64CounterMetric(StaticDimensions = "k1,k2,k3,k4,k5")]
        public static partial Counter5DNullDim CreateCounter5DNullDim(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric(StaticDimensions = "k1,k2,k4,k5", DynamicDimensions ="k3")]
        public static partial Counter5D1Change CreateCounter5D1Change(IMeter meter, string k1, string k2, string k4, string k5);

        [Int64CounterMetric(StaticDimensions = "k1,k4", DynamicDimensions ="k2,k3,k5")]
        public static partial Counter5D3Change CreateCounter5D3Change(IMeter meter, string k1, string k4);

        [Int64CounterMetric(StaticDimensions = "k1,k4,k6,k7,k8,k9,k10", DynamicDimensions = "k2,k3,k5")]
        public static partial Counter10D3Change CreateCounter10D3Change(IMeter meter, string k1, string k4, string k6, string k7, string k8, string k9, string k10);

        [Int64CounterMetric(StaticDimensions = "k1,k4,k6,k8,k10", DynamicDimensions = "k2,k3,k5,k7,k9")]
        public static partial Counter10D5Change CreateCounter10D5Change(IMeter meter, string k1, string k4, string k6, string k8, string k10);

        [Int64CounterMetric(StaticDimensions = null, DynamicDimensions = "k1,k4,k6,k8,k10,k2,k3,k5,k7,k9")]
        public static partial Counter10D10Change CreateCounter10D10Change(IMeter meter);
    }
}
