using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;

namespace EscapeHit.Invoice.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingConfiguration nlogConfig = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            //consoleTarget.Layout = "";
            nlogConfig.AddTarget("console", consoleTarget);

            var logger = NLogBuilder.ConfigureNLog(nlogConfig).GetCurrentClassLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
