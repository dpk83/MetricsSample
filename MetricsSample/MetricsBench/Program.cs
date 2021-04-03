using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace MetricsBench
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test1();

             // Test2();

            RunBenchmark(args);
        }

        static void RunBenchmark(string[] args)
        {
            var dontRequireSlnToRunBenchmarks = ManualConfig
                .Create(DefaultConfig.Instance)
                // .AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
                // .AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.DontLogOutput));
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50));

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, dontRequireSlnToRunBenchmarks);
        }

        static void Test1()
        {
            var c = new CounterBenchmark();
            c.Add_NullDimension();
        }

        static void Test2()
        {
            var c = new GenCounterBenchmark();
            //c.Add_AlternateDimensionsBetweenCalls_5DCounter();
        }
    }
}
