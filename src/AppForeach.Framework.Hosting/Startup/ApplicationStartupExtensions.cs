
using AppForeach.Framework.Hosting.Startup;

namespace Microsoft.Extensions.DependencyInjection
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
