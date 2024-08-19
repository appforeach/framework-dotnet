using AppForeach.Framework.Hosting.Web.Features.Health;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeHit.WebApi.Features;

internal static class DefaultFeatures
{
    public static void AddDefaultEscapeHitWebApiFeatures(this IServiceCollection services)
    {
        services.AddWebApplicationHealth();
    }
}
