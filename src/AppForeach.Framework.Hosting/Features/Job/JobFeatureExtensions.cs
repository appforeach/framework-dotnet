using AppForeach.Framework;
using AppForeach.Framework.Hosting.Features.Job;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class JobFeatureExtensions
{
    public static IServiceCollection AddApplicationJob<TOperation>(this IServiceCollection services, Action<IOperationBuilder>? operationOptions = null)
        where TOperation : new()
        => services.AddApplicationJob<TOperation>(TimeSpan.FromMinutes(1), operationOptions);

    public static IServiceCollection AddApplicationJob<TOperation>(this IServiceCollection services, TimeSpan interval, Action<IOperationBuilder>? operationOptions = null)
        where TOperation : new()
    {
        if(interval <= TimeSpan.Zero)
        {
            throw new ArgumentException("Interval must be positive.", nameof(interval));
        }

        services.AddSingleton(new JobFeatureOption<TOperation>
        {
            Interval = interval,
            OperationOptions = operationOptions
        });

        return services;
    }
}
