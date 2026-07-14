using AppForeach.Framework.Benchmarks.Commands.PerformTest;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Benchmarks;

public class CompiledDelegateInvocationHandlerBenchmark
{
    private IHandlerInvoker handlerInvoker;
    private ServiceProvider serviceProvider;

    [Benchmark]
    public async Task InvokeThroughCompiledDelegate()
    {
        var result = await handlerInvoker.Invoke(new PerformTestCommand(), CancellationToken.None);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddScoped(typeof(PerformTestHandler));

        var map = new CompiledHandlerMap
        ([
              new HandlerDefinition
              {
                  InputType = typeof(PerformTestCommand),
                  ImplementationMethod = typeof(PerformTestHandler).GetMethod(nameof(PerformTestHandler.Handle))
              }
        ]);
        services.AddSingleton<ICompiledHandlerMap>(map);

        services.AddFrameworkModule<FrameworkComponents>();

        services.AddScoped<IHandlerInvoker, CompiledHandlerInvoker>();

        serviceProvider = services.BuildServiceProvider();

        handlerInvoker = serviceProvider.GetRequiredService<IHandlerInvoker>();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        serviceProvider.Dispose();
    }
}
