using AppForeach.Framework.Benchmarks.Commands.PerformTest;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Benchmarks;

[MemoryDiagnoser]
public class ReflectionInvocationHandlerBenchmark
{
    private IHandlerInvoker handlerInvoker;
    private ServiceProvider serviceProvider;

    [Benchmark]
    public async Task InvokeThroughReflection()
    {
        var result = await handlerInvoker.Invoke(new PerformTestCommand(), CancellationToken.None);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped(typeof(PerformTestHandler));

        var map = new HandlerMap
        ([
              new HandlerDefinition
              {
                  InputType = typeof(PerformTestCommand),
                  ImplementationMethod = typeof(PerformTestHandler).GetMethod(nameof(PerformTestHandler.Handle))
              }
        ]);
        services.AddSingleton<IHandlerMap>(map);

        
        services.AddFrameworkModule<FrameworkComponents>();

        services.AddScoped<IHandlerInvoker, HandlerInvoker>();

        serviceProvider = services.BuildServiceProvider();

        handlerInvoker = serviceProvider.GetRequiredService<IHandlerInvoker>();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        serviceProvider.Dispose();
    }
}