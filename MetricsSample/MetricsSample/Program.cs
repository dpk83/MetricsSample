using MetricsLibrary;
using System;
using System.Collections.Generic;

namespace MetricsSample
{
    class Program
    {
        static void Main(string[] args)
        {

            //Dimensions dimensions1 = new Dimensions(("k1", "v1"));
            //Dimensions dimensions2 = new Dimensions(("k1", "v1"), ("k2", "v2"));
            //Dimensions dimensions3 = new Dimensions(("k1", "v1"), ("k2", "v2"), ("k3", "v3"));

            Dimensions dimensions = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v2"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            });

            Dimensions sameDimensionDifferentOrder = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k3", "v3"),
                new ("k5", "v5"),
                new ("k1", "v1"),
                new ("k2", "v3"),
                new ("k4", "v4"),
            });

            Dimensions missing1Dim = new Dimensions(new List<ValueTuple<string, string>>
            {
                new ("k3", "v3"),
                new ("k5", "v5"),
                new ("k2", "v2"),
                new ("k4", "v4"),
            });

            Test1();

            if (missing1Dim.HashCode != dimensions.HashCode)
            {
                Console.WriteLine("Stage 0 passed");
            }

            if (sameDimensionDifferentOrder.HashCode == dimensions.HashCode)
            {
                Console.WriteLine("Stage 1 passed");
            }

            int originalHashCode = dimensions.HashCode;
            int originalKeyHashCode = dimensions.KeysHashCode;
            CounterMetric counterMetric = new CounterMetric(dimensions);
            counterMetric.AddCounter(10, dimensions);
            counterMetric.AddCounter(20, dimensions);

            try
            {
                counterMetric.AddCounter(10, missing1Dim);
                Console.WriteLine("Stage 1.5 FAILED");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Stage 1.5 passed");
            }

            if (originalHashCode == dimensions.HashCode)
            {
                Console.WriteLine("Stage 2 passed");
            }

            dimensions["k4"] = "v5";
            if (originalHashCode != dimensions.HashCode)
            {
                Console.WriteLine("Stage 3 passed");
            }

            if (originalKeyHashCode == dimensions.KeysHashCode)
            {
                Console.WriteLine("Stage 4 passed");
            }

            counterMetric.AddCounter(5, dimensions);

            counterMetric.AddCounter(5, sameDimensionDifferentOrder);

            Console.WriteLine("Hello World!");
        }

        private static void Test1()
        {
            List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>
            {
                new ("k1", "v1"),
                new ("k2", "v3"),
                new ("k3", "v3"),
                new ("k4", "v4"),
                new ("k5", "v5"),
            };

            Dimensions dim1 = new Dimensions(list);
            Dimensions dim2 = new Dimensions(list);

            dim1["k2"] = "vnew";

            if (dim1["k2"] != dim2["k2"])
            {
                Console.WriteLine("Test1: Check 1 succeeded");
            }
            else
            {
                Console.WriteLine("Test1: Check 1 failed");
            }

        }
    }
}
