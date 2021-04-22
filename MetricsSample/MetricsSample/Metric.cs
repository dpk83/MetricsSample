using Microsoft.R9.Extensions.Meter;

namespace MetricsSample
{
    public static partial class Metric
    {
        [Int64CounterMetric(StaticDimensions = "env,host,requestName")]
        public static partial MyCounter CreateMyCounter(IMeter meter, string env, string host, string requestName);

        [Int64CounterMetric(StaticDimensions = "env,host,region,cluster", DynamicDimensions = "requestName")]
        public static partial YourCounter CreateYourCounter(IMeter meter, string env, string host, string region, string cluster);
    }
}
