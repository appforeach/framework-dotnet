using AppForeach.Framework.Hosting.Web.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppForeach.Framework.Hosting.Web
{
    public class FrameworkWebApplicationBuilder : FrameworkApplicationBuilder
    {
        private List<Action<WebApplication>> configureWebActions = new();

        public FrameworkWebApplicationBuilder(string[] args) : base(args)
        {
        }

        public void ConfigureWeb(Action<WebApplication> configureWeb)
        {
            configureWebActions.Add(configureWeb);
        }

        public override void RunApp()
        {
            var builder = WebApplication.CreateBuilder(args);
            var featureInstaller = new FrameworkWebApplicationFeatureInstaller(builder);

            ConfigureHost(builder);

            ConfigureServices(builder.Services, builder.Configuration, featureInstaller);

            var app = builder.Build();

            ConfigureWeb(app, featureInstaller);

            app.Run();
        }

        protected virtual void ConfigureHost(WebApplicationBuilder webBuilder)
        {

        }

        private void ConfigureServices(IServiceCollection services, IConfiguration configuration, FrameworkWebApplicationFeatureInstaller featureInstaller)
        {
            ConfigureServices(services);

            foreach(var configureAction in configureServicesActions)
            {
                configureAction(services, configuration);
            }

            featureInstaller.SetUpServices(services);
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }

        private void ConfigureWeb(WebApplication webApplication, FrameworkWebApplicationFeatureInstaller featureInstaller)
        {
            featureInstaller.SetUpWeb(webApplication);

            ConfigureWeb(webApplication);

            foreach (var configureAction in configureWebActions)
            {
                configureAction(webApplication);
            }
        }

        protected virtual void ConfigureWeb(WebApplication webApplication)
        {

        }
    }
}
