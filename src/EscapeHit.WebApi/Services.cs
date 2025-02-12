using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Serilog;
using EscapeHit.WebApi.Features;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeHit.WebApi
{
    internal static class Services
    {
        public static void Configure(IServiceCollection services) 
        {
            services.AddFrameworkModule<SerilogFrameworkComponents>();

            services.AddDefaultEscapeHitWebApiFeatures();

            services.AddControllers();
        }
    }
}
