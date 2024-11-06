using AppForeach.Framework.Hosting.Web;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using NLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeHit.WebApi
{
    public class EscapeHitWebApplicationBuilder : FrameworkWebApplicationBuilder
    {
        public EscapeHitWebApplicationBuilder(string[] args) : base(args)
        {
        }

        public override void Run()
        {
            LoggingConfiguration nlogConfig = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            nlogConfig.AddTarget("console", consoleTarget);
            var logger = NLogBuilder.ConfigureNLog(nlogConfig).GetCurrentClassLogger();

            try
            {
                base.Run();
            }
#if !DEBUG
            catch (Exception ex)
            {
                logger.Error(ex);
            }
#endif
            finally
            {
                LogManager.Shutdown();
            } 
        }

        protected override void ConfigureHost(WebApplicationBuilder webBuilder)
        {
            base.ConfigureHost(webBuilder);

            webBuilder.Host.UseNLog();

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
