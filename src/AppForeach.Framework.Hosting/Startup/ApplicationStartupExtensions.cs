using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Startup
{
    public static class ApplicationStartupExtensions
    {
        public static void AddApplicationStartup<TStartup>(this IServiceCollection services)
            where TStartup : class, IApplicationStartup
        {
            services.AddScoped<TStartup>();
            services.AddSingleton<IApplicationStartupDescriptor>(new ApplicationStartupDescriptor<TStartup>());
        }
    }
}
