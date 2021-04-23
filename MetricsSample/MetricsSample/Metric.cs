using Microsoft.R9.Extensions.Meter;

namespace MetricsSample
{
    public static partial class Metric
    {
        [Int64CounterMetric(Dimensions = "env,host,requestName")]
        public static partial MyCounter CreateMyCounter(IMeter meter);

        [Int64CounterMetric(Dimensions = "env,host,region,cluster, requestName")]
        public static partial YourCounter CreateYourCounter(IMeter meter);
    }
}
