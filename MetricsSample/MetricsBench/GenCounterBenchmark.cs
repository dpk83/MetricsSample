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
    public class GenCounterBenchmark_3
    {
        private IMeter _meter;
        private Counter5DNullDim _counter5DNullDim;
        private Counter5D1Change _counter5D1Change;
        private Counter5D3Change _counter5D3Change;
        private Counter10D3Change _counter10D3Change;
        private Counter10D5Change _counter10D5Change;
        private Counter10D10Change _counter10D10Change;
        private string[] _valuesToFeed = { "feed1", "feed2", "feed3", "feed4", "feed5", "feed6" };

        static long value1 = 0;
        static long value2 = 0;

        public GenCounterBenchmark_3()
        {
            _meter = new GenevaMeter("testMeter", new MdmMetricFactory(), "testMonitoringAccount");
            _counter5DNullDim = (Counter5DNullDim)Metric.CreateCounter5DNullDim(_meter);
            _counter5D1Change = (Counter5D1Change)Metric.CreateCounter5D1Change(_meter);
            _counter5D3Change = (Counter5D3Change)Metric.CreateCounter5D3Change(_meter);

            _counter10D3Change = (Counter10D3Change)Metric.CreateCounter10D3Change(_meter);
            _counter10D5Change = (Counter10D5Change)Metric.CreateCounter10D5Change(_meter);
            _counter10D10Change = (Counter10D10Change)Metric.CreateCounter10D10Change(_meter);
        }

        [Benchmark]
        public void Add_NullDimension()
        {
            value1++;
            _counter5DNullDim.Add(value1, _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0]);
        }

        [Benchmark]
        public void Add_NoDimensionChanges()
        {
            value1++;
            _counter5DNullDim.Add(value1, _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0], _valuesToFeed[0]);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            // value2++;

            _counter5D1Change.Add(value2, _valuesToFeed[0], _valuesToFeed[1], _valuesToFeed[2], _valuesToFeed[3], _valuesToFeed[value2 % 5]);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            // value2++;

            _counter5D3Change.Add(value2, _valuesToFeed[0], _valuesToFeed[1], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5]);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            // value2++;
            _counter10D3Change.Add(value2,
                _valuesToFeed[0], _valuesToFeed[1], _valuesToFeed[2], _valuesToFeed[3], _valuesToFeed[4],
                _valuesToFeed[5], _valuesToFeed[6], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5]);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            // value2++;
            _counter10D5Change.Add(value2,
                _valuesToFeed[0], _valuesToFeed[1], _valuesToFeed[2], _valuesToFeed[3], _valuesToFeed[4],
                _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5]);
        }

        [Benchmark]
        public void Add_Update10DimValueInEachCall_10DCounter()
        {
            // value2++;

            _counter10D10Change.Add(value2,
                _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5],
                _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5], _valuesToFeed[value2 % 5]);
        }
    }
}
