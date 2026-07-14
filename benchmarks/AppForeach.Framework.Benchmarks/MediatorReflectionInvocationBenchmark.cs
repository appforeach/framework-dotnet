using AppForeach.Framework.Benchmarks.Commands.PerformTest;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Benchmarks;

public class MediatorReflectionInvocationBenchmark
{
    private ServiceProvider serviceProvider;
    private IOperationMediator mediator;

    [Benchmark]
    public async Task InvokeMediatorThroughReflection()
    {
        await mediator.Execute(new PerformTestCommand()).As<PerformTestResult>();
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var services = new ServiceCollection();

        services.AddFrameworkModule<FrameworkComponents>();

        services.AddSingleton<IFrameworkHostConfiguration>(sp => {
            FrameworkHostConfiguration hostConfig = new FrameworkHostConfiguration();
            return hostConfig;
        });

        services.AddOperationHandlers([typeof(MediatorCompiledInvocationBenchmark).Assembly]);

        services.AddScoped<IHandlerInvoker, HandlerInvoker>();

        serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();

        mediator = scope.ServiceProvider.GetRequiredService<IOperationMediator>();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        serviceProvider.Dispose();
    }
}
