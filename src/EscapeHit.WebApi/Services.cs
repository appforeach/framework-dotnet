using AppForeach.Framework.Hosting.Features.Logging;
using AppForeach.Framework.Logging;
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
            services.AddSingleton<ILoggingPropertyMap, EcsLoggingPropertyMap>();
            services.AddApplicationConfigurationLoggingProperty<string>("service.name", "app:name");
            services.AddApplicationConfigurationLoggingProperty("service.environment", "app:environment", "local");

            services.AddDefaultEscapeHitWebApiFeatures();

            services.AddControllers();
        }
    }
}
