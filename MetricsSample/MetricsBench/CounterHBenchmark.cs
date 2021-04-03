
using BenchmarkDotNet.Attributes;
using MetricsLibrary;
using Microsoft.Cloud.InstrumentationFramework.Metrics.Extensions;
using Microsoft.R9.Extensions.Meter.Geneva;
using System;
using System.Collections.Generic;

namespace MetricsBench
{
    [GcServer(true)]
    [MinColumn]
    [MaxColumn]
    [MemoryDiagnoser]
    public class CounterHBenchmark
    {
        HDimensions dimensions = new HDimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            });

        HDimensions dimensions10D = new HDimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
                new ("k6", "v1"),
                new ("k7", "v2"),
                new ("k8", "v3"),
                new ("k9", "v4"),
                new ("k10", "v5"),
            });

        HDimensions dimensions20D = new HDimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v11"),
                new ("k2", "v21"),
                new ("k3", "v31"),
                new ("k4", "v41"),
                new ("k5", "v51"),
                new ("k6", "v11"),
                new ("k7", "v21"),
                new ("k8", "v31"),
                new ("k9", "v41"),
                new ("k10", "v5"),
                new ("k11", "v11"),
                new ("k12", "v21"),
                new ("k13", "v31"),
                new ("k14", "v41"),
                new ("k15", "v51"),
                new ("k16", "v11"),
                new ("k17", "v21"),
                new ("k18", "v31"),
                new ("k19", "v41"),
                new ("k20", "v51"),
            });

        private GenevaMeter _meter;
        CounterMetricH<DimensionValues5D> counterMetric5D;
        CounterMetricH<DimensionValues10D> counterMetric10D;
        static int value1 = 0;
        static int value2 = 0;
        static ulong index = 0;

        public CounterHBenchmark()
        {
            _meter = new GenevaMeter("testMeter", new MdmMetricFactory(), "testMonitoringAccount");

            var meterOptions = _meter.MeterOptions;
            var cumulativeMetric5D = meterOptions.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        meterOptions.MonitoringAccount,
                                        meterOptions.MetricNamespace,
                                        "counter5D",
                                        "k1", "k2", "k3", "k4", "k5");

            var cumulativeMetric10D = meterOptions.MdmMetricFactory.CreateUInt64CumulativeMetric(
                                        MdmMetricFlags.CumulativeMetricDefault,
                                        meterOptions.MonitoringAccount,
                                        meterOptions.MetricNamespace,
                                        "counter5D",
                                        "k1", "k2", "k3", "k4", "k5", "k6", "k7", "k8", "k9", "k10");

            counterMetric5D = new CounterMetricH<DimensionValues5D>(cumulativeMetric5D, dimensions);
            counterMetric10D = new CounterMetricH<DimensionValues10D>(cumulativeMetric10D, dimensions10D);
        }

        [Benchmark]
        public void Add_NullDimension()
        {
            counterMetric5D.Add(value1++, null);
        }

        [Benchmark]
        public void Add_NoDimensionChanges()
        {
            counterMetric5D.Add(value1++, dimensions);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            value2++;
            string val = index++.ToString();
            dimensions["k3"] = val;
            counterMetric5D.Add(value2, dimensions);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            value2++;
            string val = index++.ToString();
            dimensions["k3"] = val;
            dimensions["k5"] = val;
            dimensions["k2"] = val;
            counterMetric5D.Add(value2, dimensions);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            value2++;
            string val = index++.ToString();
            dimensions10D["k3"] = val;
            dimensions10D["k5"] = val;
            dimensions10D["k10"] = val;
            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            value2++;
            string val = index++.ToString();
            dimensions10D["k1"] = val;
            dimensions10D["k3"] = val;
            dimensions10D["k6"] = val;
            dimensions10D["k5"] = val;
            dimensions10D["k10"] = val;
            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update10DimValueInEachCall_10DCounter()
        {
            value2++;
            string val = index++.ToString();
            dimensions10D["k1"] = val;
            dimensions10D["k3"] = val;
            dimensions10D["k6"] = val;
            dimensions10D["k5"] = val;
            dimensions10D["k10"] = val;
            dimensions10D["k2"] = val;
            dimensions10D["k4"] = val;
            dimensions10D["k7"] = val;
            dimensions10D["k8"] = val;
            dimensions10D["k9"] = val;
            counterMetric10D.Add(value2, dimensions10D);
        }
    }
}
