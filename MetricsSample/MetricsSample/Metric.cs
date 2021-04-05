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
        [Int64CounterMetric("MyCounter", "env,host,requestName")]
        public static partial MyCounter CreateMyCounter(IMeter meter, string env, string host, string requestName);

        [Int64CounterMetric("YourCounter", "env,host,region,cluster", "requestName")]
        public static partial YourCounter CreateYourCounter(IMeter meter, string env, string host, string region, string cluster);
    }
}
