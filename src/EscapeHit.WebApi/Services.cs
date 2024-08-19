using EscapeHit.WebApi.Features;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeHit.WebApi
{
    internal static class Services
    {
        public static void Configure(IServiceCollection services) 
        {
            services.AddDefaultEscapeHitWebApiFeatures();

            services.AddControllers();
        }
    }
}
