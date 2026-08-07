using AppForeach.Framework.Benchmarks.Commands.PerformTest;
using BenchmarkDotNet.Attributes;

namespace AppForeach.Framework.Benchmarks;

[MemoryDiagnoser]
public class DirectInvocationBenchmark
{

    [Benchmark]
    public async Task InvokeDirectly()
    {
        var handler = new PerformTestHandler();
        var result = await handler.Handle(new PerformTestCommand(), CancellationToken.None);
    }
}
