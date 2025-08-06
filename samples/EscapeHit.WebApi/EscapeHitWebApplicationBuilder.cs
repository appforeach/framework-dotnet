using AppForeach.Framework.Hosting.Features.Serilog.Ecs;
using AppForeach.Framework.Hosting.Web;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;

namespace EscapeHit.WebApi
{
    public class EscapeHitWebApplicationBuilder : FrameworkWebApplicationBuilder
    {
        private Serilog.ILogger logger;

        public EscapeHitWebApplicationBuilder(string[] args) : base(args)
        {
        }

        public override void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.FromLogContext()
                .WriteTo.Console(new EcsSerilogTextFormatter())
                .CreateLogger();

            try
            {
                base.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        protected override void ConfigureHost(WebApplicationBuilder webBuilder)
        {
            base.ConfigureHost(webBuilder);

            webBuilder.Host.UseSerilog((context, services, configure) =>
            {
                configure
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .Enrich.WithElasticApmCorrelationInfo()
                    .Enrich.FromLogContext()
                    .ReadFrom.Services(services)
                    .WriteTo.Console(new EcsSerilogTextFormatter());
            });

            webBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            ConfigureServices(Services.Configure);
        }

        protected override void ConfigureWeb(WebApplication webApplication)
        {
            base.ConfigureWeb(webApplication);

            ConfigureWeb(Web.Configure);
        }
    }
}
