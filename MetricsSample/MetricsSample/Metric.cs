using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.MetricUtilities;

namespace MetricsSample
{
    public static partial class Metric
    {
        // [MetricGen(1, MetricType.Int64, @"{env} {region} {}")]
        // [Counter]
        //public void requestAvailabilityCounter(int value, string env, string region, string appName, string requestName, string reqHost, string reqResultCode)
        //{ 
        //}

        // [Counter(0)]
        // public static partial void MyCounter(string clusterId, string requestId);

        [Int64CounterMetric("myCounterMetric")]
        public static partial MyCounter CreateMyCounter(IMeter meter, string env, string host, string requestName);

        [Int64CounterMetric("yourCounter")]
        public static partial YourCounter CreateYourCounter(IMeter meter, string env, string host, string requestName, string region, string cluster);
    }
}
