
using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricUtilities;

namespace MetricsBench
{
    public static partial class Metric
    {
        [Int64CounterMetric("Counter5D", "k1,k2,k3,k4,k5")]
        public static partial Counter5D CreateCounter5D(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric("Counter10D", "k1,k2,k3,k4,k5,k6,k7,k8,k9,k10")]
        public static partial Counter10D CreateCounter10D(IMeter meter, string k1, string k2, string k3, string k4, string k5, string k6, string k7, string k8, string k9, string k10);

        [Int64CounterMetric("Counter5DNullDim", "k1,k2,k3,k4,k5")]
        public static partial Counter5DNullDim CreateCounter5DNullDim(IMeter meter, string k1, string k2, string k3, string k4, string k5);

        [Int64CounterMetric("Counter5D1Change", "k1,k2,k4,k5", "k3")]
        public static partial Counter5D1Change CreateCounter5D1Change(IMeter meter, string k1, string k2, string k4, string k5);

        [Int64CounterMetric("Counter5D3Change", "k1,k4", "k2,k3,k5")]
        public static partial Counter5D3Change CreateCounter5D3Change(IMeter meter, string k1, string k4);

        [Int64CounterMetric("Counter10D3Change", "k1,k4,k6,k7,k8,k9,k10", "k2,k3,k5")]
        public static partial Counter10D3Change CreateCounter10D3Change(IMeter meter, string k1, string k4, string k6, string k7, string k8, string k9, string k10);

        [Int64CounterMetric("Counter10D5Change", "k1,k4,k6,k8,k10", "k2,k3,k5,k7,k9")]
        public static partial Counter10D5Change CreateCounter10D5Change(IMeter meter, string k1, string k4, string k6, string k8, string k10);

        [Int64CounterMetric("Counter10D10Change", null, "k1,k4,k6,k8,k10,k2,k3,k5,k7,k9")]
        public static partial Counter10D10Change CreateCounter10D10Change(IMeter meter);


        //[Int64CounterMetric("RequestVolumeCounter", "env,host,region")]
        //public static partial ICounterMetric<long> CreateRequestVolumeCounter(IMeter meter, string env, string host, string region, string tenantId, string requestName);
    }
}
