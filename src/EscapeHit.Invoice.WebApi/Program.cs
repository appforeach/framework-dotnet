using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppForeach.Framework;
using AppForeach.Framework.EntityFrameworkCore;
using EscapeHit.Invoice.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                .UseWindsorContainerServiceProvider()
                .UseNLog()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IConnectionStringProvider>(sp => new ConnectionStringProvider("Server=(local); Initial Catalog=invoice; Integrated Security=true; MultipleActiveResultSets=True;"));
                    services.AddScoped(sp =>
                    {
                        var operationContext = sp.GetRequiredService<IOperationContext>();
                        var transationState = operationContext.State.Get<TransactionScopeState>();

                        var optionsBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
                        optionsBuilder.UseSqlServer(transationState.DbContext.Database.GetDbConnection());

                        var db = new InvoiceDbContext(optionsBuilder.Options);
                        db.Database.UseTransaction(transationState.DbContextTransaction.GetDbTransaction());
                        
                        return db;
                    });
                    //services.AddDbContext<InvoiceDbContext>(options => options.UseSqlServer("Server=(local); Initial Catalog=invoice; Integrated Security=true; MultipleActiveResultSets=True;"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            ;
    }
}
