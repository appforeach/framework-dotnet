
using AppForeach.Framework.Hosting.Startup;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationStartupExtensions
    {
        public static void AddApplicationStartup<TStartup>(this IServiceCollection services, Action<IApplicationStartupOptionsConfigurator>? optionsConfigurator = null)
            where TStartup : class, IApplicationStartup
        {
            ApplicationStartupOptions startupOptions = new ApplicationStartupOptions();

            ApplicationStartupOptionsConfigurator configurator = new ApplicationStartupOptionsConfigurator(startupOptions);
            optionsConfigurator?.Invoke(configurator);

            services.AddScoped<TStartup>();
            services.AddSingleton<IApplicationStartupDescriptor>(new ApplicationStartupDescriptor<TStartup>()
            {
                Options = startupOptions
            });
        }
    }
}
