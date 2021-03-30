using BenchmarkDotNet.Attributes;
using MetricsLibrary;
using System;
using System.Collections.Generic;

namespace MetricsBench
{
    [GcServer(true)]
    [MinColumn]
    [MaxColumn]
    [MemoryDiagnoser]
    public class CounterBenchmark
    {
        Dimensions dimensions = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            });

        Dimensions dimensions2 = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v31"),
                new ("k4", "v41"),
                new ("k5", "v5"),
            });

        Dimensions changingDimensions = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            });

        Dimensions dimensions20D = new Dimensions(new List<ValueTuple<string, string>>
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

        CounterMetric counterMetric;
        CounterMetric counterMetric20D;
        static int value1 = 0;
        static int value2 = 0;
        static ulong index = 0;

        public CounterBenchmark()
        {
            counterMetric = new CounterMetric(dimensions);
            counterMetric20D = new CounterMetric(dimensions20D);
        }

        //[IterationSetup]
        //public void IterationSetup()
        //{ value1 = 0; value2 = 0; index = 0; }

        //[IterationCleanup]
        //public void IterationCleanup()
        //{ value1 = 0; value2 = 0; index = 0; }

        [Benchmark]
        public void AddCounter_NoDimensionChanges()
        {
            counterMetric.AddCounter(value1++, dimensions);
        }

        [Benchmark]
        public void AddCounter_AlternateDimensionsBetweenCalls()
        {
            value2 = value2++;
            counterMetric.AddCounter(value2, (value2 % 2 == 0) ? dimensions2 : dimensions);
        }

        [Benchmark]
        public void AddCounter_DifferentDimensionValuesInEachCall_SingleDimensionChange()
        {
            changingDimensions["k3"] = index++.ToString();
            counterMetric.AddCounter(value2, changingDimensions);
        }

        [Benchmark]
        public void AddCounter_DifferentDimensionValuesInEachCall_MultiDimensionChange()
        {
            changingDimensions["k3"] = index++.ToString();
            changingDimensions["k5"] = index++.ToString();
            changingDimensions["k2"] = index++.ToString();
            counterMetric.AddCounter(value2, changingDimensions);
        }

        [Benchmark]
        public void AddCounter_MultiDimensionChange_20D()
        {
            dimensions20D["k13"] = index++.ToString();
            dimensions20D["k15"] = index++.ToString();
            dimensions20D["k20"] = index++.ToString();
            counterMetric20D.AddCounter(value2, dimensions20D);
        }
    }
}
