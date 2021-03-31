using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using MetricsLibrary;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsBench
{
    [GcServer(true)]
    [MinColumn]
    [MaxColumn]
    [MemoryDiagnoser]
    public class OldCounterBenchmark
    {
        ISet<string> keys = new HashSet<string>
        {
            "k1", "k2", "k3", "k4", "k5"
        };

        ISet<string> keys10D = new HashSet<string>
        {
            "k11", "k12", "k13", "k14", "k15", "k16", "k17", "k18", "k19", "k20"
        };

        ISet<string> keys20D = new HashSet<string>
        {
            "k1", "k2", "k3", "k4", "k5", "k6", "k7", "k8", "k9", "k10", "k11", "k12", "k13", "k14", "k15", "k16", "k17", "k18", "k19", "k20"
        };

        List<ValueTuple<string, string>> dimensions1 = new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            };

        List<ValueTuple<string, string>> dimensions2 = new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v31"),
                new ("k4", "v41"),
                new ("k5", "v5"),
            };

        List<ValueTuple<string, string>> dimensions1Update = new List<ValueTuple<string, string>>
            {
                new ("k3", "v30"),
            };

        List<ValueTuple<string, string>> dimensions2Update = new List<ValueTuple<string, string>>
            {
                new ("k4", "v41"),
            };

        List<ValueTuple<string, string>> dimensions10D = new List<ValueTuple<string, string>>
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
            };

        List<ValueTuple<string, string>> dimensions20D = new List<ValueTuple<string, string>>
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
            };

        CounterMetricOld counterMetric;
        CounterMetricOld counterMetric10D;
        CounterMetricOld counterMetric20D;
        static int value1 = 0;
        static int value2 = 0;
        static ulong index = 0;

        public OldCounterBenchmark()
        {
            counterMetric = new CounterMetricOld(keys, dimensions1);
            counterMetric10D = new CounterMetricOld(keys10D, dimensions10D);
            counterMetric20D = new CounterMetricOld(keys20D, dimensions20D);
        }

        [Benchmark]
        public void Add_NullDimension()
        {
            counterMetric.Add(value1++, null);
        }

        [Benchmark]
        public void Add_NoDimensionChanges()
        {
            counterMetric.Add(value1++, dimensions1);
        }

        [Benchmark]
        public void Add_AlternateDimensionsBetweenCalls_5DCounter()
        {
            value2 = value2++;
            counterMetric.Add(value2, (value2 % 2 == 0) ? dimensions1Update : dimensions2Update);
        }

        [Benchmark]
        public void Add_Update1DimValueInEachCall_5DCounter()
        {
            List<ValueTuple<string, string>> updatedDim = new() { new("k3", index++.ToString())};
            counterMetric.Add(value2, updatedDim);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_5DCounter()
        {
            List<ValueTuple<string, string>> updatedDim = new() 
            { 
                new("k3", index++.ToString()),
                new("k5", index++.ToString()),
                new("k2", index++.ToString())
            };

            counterMetric.Add(value2, updatedDim);
        }

        [Benchmark]
        public void Add_Update3DimValueInEachCall_10DCounter()
        {
            List<ValueTuple<string, string>> updatedDim = new()
            {
                new("k13", index++.ToString()),
                new("k15", index++.ToString()),
                new("k19", index++.ToString())
            };

            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_10DCounter()
        {
            List<ValueTuple<string, string>> updatedDim = new()
            {
                new("k12", index++.ToString()),
                new("k13", index++.ToString()),
                new("k16", index++.ToString()),
                new("k15", index++.ToString()),
                new("k19", index++.ToString())
            };

            counterMetric10D.Add(value2, dimensions10D);
        }

        [Benchmark]
        public void Add_Update5DimValueInEachCall_20DCounter()
        {
            List<ValueTuple<string, string>> updatedDim = new()
            {
                new("k2", index++.ToString()),
                new("k5", index++.ToString()),
                new("k13", index++.ToString()),
                new("k15", index++.ToString()),
                new("k19", index++.ToString())
            };

            counterMetric20D.Add(value2, dimensions20D);
        }

        [Benchmark]
        public void Add_NoDimensionChanges_NewDimensionList()
        {
            List<ValueTuple<string, string>> dimensions = new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            };

            counterMetric.Add(value1, dimensions);
        }
    }
}
