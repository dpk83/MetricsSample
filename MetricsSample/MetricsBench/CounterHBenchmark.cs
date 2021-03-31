﻿
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

        HDimensions dimensions2 = new HDimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v31"),
                new ("k4", "v41"),
                new ("k5", "v5"),
            });

        HDimensions dimensions10D = new HDimensions(new List<ValueTuple<string, string>>
            {
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

        CounterMetricH counterMetric5D;
        CounterMetricH counterMetric10D;
        CounterMetricH counterMetric20D;
        static int value1 = 0;
        static int value2 = 0;
        static ulong index = 0;

        public CounterHBenchmark()
        {
            counterMetric5D = new CounterMetricH(dimensions);
            counterMetric10D = new CounterMetricH(dimensions10D);
            counterMetric20D = new CounterMetricH(dimensions20D);
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
        public void Add_AlternateUnsortedDimensionsBetweenCalls_5DCounter()
        {
            value2 = value2++;
            counterMetric5D.Add(value2, (value2 % 2 == 0) ? dimensions2 : dimensions);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            dimensions["k3"] = index++.ToString();
            counterMetric5D.Add(value2, dimensions);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            dimensions["k3"] = index++.ToString();
            dimensions["k5"] = index++.ToString();
            dimensions["k2"] = index++.ToString();
            counterMetric5D.Add(value2, dimensions);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            dimensions10D["k13"] = index++.ToString();
            dimensions10D["k15"] = index++.ToString();
            dimensions10D["k20"] = index++.ToString();
            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            dimensions10D["k11"] = index++.ToString();
            dimensions10D["k13"] = index++.ToString();
            dimensions10D["k16"] = index++.ToString();
            dimensions10D["k15"] = index++.ToString();
            dimensions10D["k20"] = index++.ToString();
            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_20DCounter()
        {
            dimensions20D["k2"] = index++.ToString();
            dimensions20D["k5"] = index++.ToString();
            dimensions20D["k13"] = index++.ToString();
            dimensions20D["k15"] = index++.ToString();
            dimensions20D["k20"] = index++.ToString();
            counterMetric20D.Add(value2, dimensions20D);
        }

        [Benchmark]
        public void Add_NoDimensionChanges_NewDimensionObject()
        {
            HDimensions newUnsortedDimensions = new HDimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            });
            counterMetric5D.Add(value1, newUnsortedDimensions);
        }
    }
}