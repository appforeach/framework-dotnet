using AppForeach.Framework;
using AppForeach.Framework.Hosting.Features.Mediator;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection;

public static class MediatorFeatureExtensions
{
    public static void AddApplicationMediator(this IServiceCollection services, Action<IOperationBuilder>? applicationOptions = null, List<Type>? middlewares = null)
    {
        services.AddSingleton(new MediatorFeatureOption
        {
            ApplicationOptions = applicationOptions,
            GetMiddlewares = middlewares != null ? hasDatabase => middlewares : null
        });
    }

    public static void AddApplicationMediator(this IServiceCollection services, Action<IOperationBuilder>? applicationOptions = null, Func<bool, List<Type>>? getMiddlewares = null)
    {
        services.AddSingleton(new MediatorFeatureOption
        {
            ApplicationOptions = applicationOptions,
            GetMiddlewares = getMiddlewares
        });
    }
}
