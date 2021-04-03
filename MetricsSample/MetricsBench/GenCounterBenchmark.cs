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
    public class GenCounterBenchmark
    {
        private IMeter _meter;
        private Counter5D _counter5D;
        private Counter10D _counter10D;

        static int value1 = 0;
        static int value2 = 0;
        static ulong index = 0;

        public GenCounterBenchmark()
        {
            _meter = new GenevaMeter("testMeter", new MdmMetricFactory(), "testMonitoringAccount");
            _counter5D = Metric.Create5dCounter(_meter, "v1", "v2", "v3", "v4", "v5");
            _counter10D = Metric.Create10dCounter(_meter, "v1", "v2", "v3", "v4", "v5", "v6", "v7", "v8", "v9", "v10");
        }

        //[IterationSetup]
        //public void IterationSetup()
        //{ val = index++.ToString(); }

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
        public void Add_AlternateDimensionsBetweenCalls_5DCounter()
        {
            index++;
            value2++;
            _counter5D.k2 = index.ToString();
            _counter5D.Add(value2);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            index++;
            value2++;
            _counter5D.k3 = index.ToString();
            _counter5D.Add(value2);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            value2++;
            string val = index++.ToString();
            _counter5D.k3 = val;
            _counter5D.k5 = val;
            _counter5D.k2 = val;

            _counter5D.Add(value2);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            value2++;
            string val = index++.ToString();
            _counter10D.k3 = val;
            _counter10D.k5 = val;
            _counter10D.k2 = val;

            _counter10D.Add(value2);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            value2++;
            string val = index++.ToString();
            _counter10D.k3 = val;
            _counter10D.k5 = val;
            _counter10D.k2 = val;
            _counter10D.k9 = val;
            _counter10D.k7 = val;

            _counter10D.Add(value2);
        }
    }
}
