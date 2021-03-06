using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using System.Text.RegularExpressions;

namespace MetricsBench
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test();
            // Test1();
             // Test2();
            RunBenchmark(args);
        }

        static void RunBenchmark(string[] args)
        {
            var dontRequireSlnToRunBenchmarks = ManualConfig
                .Create(DefaultConfig.Instance)
                //.AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
                .AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.DontLogOutput));
                //.AddJob(Job.Default.WithRuntime(CoreRuntime.Core50));

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, dontRequireSlnToRunBenchmarks);
        }

        static void Test()
        {
            var metric = new GenCounterBenchmark_3();
            metric.Add_NullDimension();
            metric.Add_NoDimensionChanges();
            metric.Add_Update1DimValueInEachCall_5DCounter();
            metric.Add_Update3DimValueInEachCall_5DCounter();
            metric.Add_Update3DimValueInEachCall_10DCounter();
            metric.Add_Update5DimValueInEachCall_10DCounter();
            metric.Add_Update10DimValueInEachCall_10DCounter();
        }

        static void Test1()
        {
            //var c = new CounterBenchmark();
            //c.Add_NullDimension();
        }

        static void Test2()
        {
            //string staticDimensions = string.Empty;
            //var tokens = Regex.Split(staticDimensions, ",");

            //var b = new GenCounterBenchmark_3();
            //var c = new GenCounterBenchmark_3();
            //c.Add_Update3DimValueInEachCall_10DCounter();
        }
    }
}
