using AppForeach.Framework.Hosting.Web.Features.Health;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthFeatureExtensions
{
    public static void AddWebApplicationHealth(this IServiceCollection services)
    {
        services.AddSingleton(new HealthFeatureOption());
    }
}
