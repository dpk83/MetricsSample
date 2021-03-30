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
            var dontRequireSlnToRunBenchmarks = ManualConfig
                .Create(DefaultConfig.Instance)
                //.AddJob(Job.ShortRun.WithToolchain(InProcessEmitToolchain.Instance));
                .AddJob(Job.Default.WithToolchain(InProcessEmitToolchain.DontLogOutput));

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, dontRequireSlnToRunBenchmarks);

            //var c = new SortedCounterBenchmark();
            //c.AddCounter_MultiDimensionChange_20D();
        }
    }
}
