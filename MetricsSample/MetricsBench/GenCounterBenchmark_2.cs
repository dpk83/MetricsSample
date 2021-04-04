using BenchmarkDotNet.Attributes;
using MetricsLibrary;
using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using Microsoft.R9.Extensions.Meter;
using Microsoft.R9.Extensions.Meter.Geneva;
using System;
using System.Collections.Generic;

namespace MetricsBench
{
    [GcServer(true)]
    [MinColumn]
    [MaxColumn]
    [MemoryDiagnoser]
    public class GenCounterBenchmark_2
    {
        private IMeter _meter;
        private ICounterMetric<long> _counter5D;
        private ICounterMetric<long> _counter10D;
        private string[] _valuesToFeed = { "feed1", "feed2", "feed3", "feed4", "feed5" };

        static int value1 = 0;
        static int value2 = 0;

        public GenCounterBenchmark_2()
        {
            _meter = new GenevaMeter("testMeter", new MdmMetricFactory(), "testMonitoringAccount");
            _counter5D = Metric.Create5dCounter(_meter, "v1", "v2", "v3", "v4", "v5");
            _counter10D = Metric.Create10dCounter(_meter, "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8", "v9", "v10");
        }

        [Benchmark]
        public void Add_NullDimension()
        {
            value1++;
            _counter5D.Add(value1);
        }

        [Benchmark]
        public void Add_NoDimensionChanges()
        {
            value1++;
            _counter5D.Add(value1);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            value2++;

            _counter5D["k3"] = _valuesToFeed[value2 % 5];
            _counter5D.Add(value2);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            value2++;

            _counter5D["k3"] = _valuesToFeed[value2 % 5];
            _counter5D["k5"] = _valuesToFeed[value2 % 5];
            _counter5D["k2"] = _valuesToFeed[value2 % 5];

            _counter5D.Add(value2);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            value2++;

            _counter10D["k3"] = _valuesToFeed[value2 % 5];
            _counter10D["k5"] = _valuesToFeed[value2 % 5];
            _counter10D["k2"] = _valuesToFeed[value2 % 5];

            _counter10D.Add(value2);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            value2++;

            _counter10D["k3"] = _valuesToFeed[value2 % 5];
            _counter10D["k5"] = _valuesToFeed[value2 % 5];
            _counter10D["k2"] = _valuesToFeed[value2 % 5];
            _counter10D["k9"] = _valuesToFeed[value2 % 5];
            _counter10D["k7"] = _valuesToFeed[value2 % 5];

            _counter10D.Add(value2);
        }

        [Benchmark]
        public void Add_Update10DimValueInEachCall_10DCounter()
        {
            value2++;

            _counter10D["k3"] = _valuesToFeed[value2 % 5];
            _counter10D["k5"] = _valuesToFeed[value2 % 5];
            _counter10D["k2"] = _valuesToFeed[value2 % 5];
            _counter10D["k9"] = _valuesToFeed[value2 % 5];
            _counter10D["k7"] = _valuesToFeed[value2 % 5];
            _counter10D["k1"] = _valuesToFeed[value2 % 5];
            _counter10D["k4"] = _valuesToFeed[value2 % 5];
            _counter10D["k6"] = _valuesToFeed[value2 % 5];
            _counter10D["k8"] = _valuesToFeed[value2 % 5];
            _counter10D["k10"] = _valuesToFeed[value2 % 5];

            _counter10D.Add(value2);
        }
    }
}
