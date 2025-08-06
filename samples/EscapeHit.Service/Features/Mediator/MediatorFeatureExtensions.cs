using AppForeach.Framework;
using System;
using EscapeHit.Service.Features.Mediator;

namespace Microsoft.Extensions.DependencyInjection;
public static class MediatorFeatureExtensions
{
    public static void AddEscapeHitMediator(this IServiceCollection services, Action<IOperationBuilder> applicationOptions = null)
    {
        services.AddApplicationMediator(opt =>
        {
            DefaultOperationOptions.ConfigureDefault(opt);
            applicationOptions?.Invoke(opt);
        }, DefaultMiddlewares.GetDefault);
    }
}
