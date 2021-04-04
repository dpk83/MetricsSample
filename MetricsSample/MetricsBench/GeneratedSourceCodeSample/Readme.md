## What Developer writes
```
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
```

## What gets generated
2 source code files. 
 - GeneratedGenevaMeter
 - MetricGenerator

## How application code will use it

ICounterMetric<long> counterMetric = Metric.Create5dCounter(meter, "v1", "v2", "v3", "v4", "v5");

counterMetric.Add(1);

counterMetric["k2"] = "newv2";
counterMetric["k4"] = "newv4";
counteMetric.ADd(1);

OR cast it to the actual concrete type and directly access the keys as properties. 
This option is faster as it doesn't need to iterate through to find the right key to update

Counter5D counterMetric = (Counter5D) Metric.Create5dCounter(meter, "v1", "v2", "v3", "v4", "v5");

counterMetric.k2 = "newv2";
counterMetric.k4 = "newv4";

