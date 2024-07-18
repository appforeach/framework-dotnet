using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Mediator
{
    public static class MediatorFeatureExtensions
    {
        public static void AddApplicationMediator(this IServiceCollection services, Action<IOperationBuilder>? applicationOptions = null, List<Type>? middlewares = null)
        {
            services.AddSingleton(new MediatorFeatureOption
            {
                ApplicationOptions = applicationOptions,
                Middlewares = middlewares
            });
        }
    }
}
