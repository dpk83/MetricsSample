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
            RunBenchmark(args);
        }

        static void RunBenchmark(string[] args)
        {
            var dontRequireSlnToRunBenchmarks = ManualConfig
                .Create(DefaultConfig.Instance)
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50));

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, dontRequireSlnToRunBenchmarks);
        }
    }
}
