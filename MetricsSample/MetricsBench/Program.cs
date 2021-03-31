using BenchmarkDotNet.Configs;
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
                //.AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
                .AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.DontLogOutput));

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, dontRequireSlnToRunBenchmarks);
        }

        static void Test1()
        {
            var c = new CounterBenchmark();
            c.Add_NullDimension();
        }

        static void Test2()
        {
            var c = new OldCounterBenchmark();
            c.Add_NullDimension();
        }
    }
}
