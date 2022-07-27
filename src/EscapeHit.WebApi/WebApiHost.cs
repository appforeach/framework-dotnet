using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppForeach.Framework;
using AppForeach.Framework.Castle.Windsor;
using AppForeach.Framework.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
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

namespace EscapeHit.WebApi
{
    public class WebApiHost : IWebApiHost
    {
        private readonly string[] args;
        private Action<IServiceCollection> dbContextRegistrationAction;
        private List<Action<IWindsorContainer>> windsorRegistrationActions = new List<Action<IWindsorContainer>>();

        private Type webStartupType;

        private WebApiHost(string[] args)
        {
            this.args = args;
        }

        public static IWebApiHost Create(string[] args)
        {
            return new WebApiHost(args);
        }

        public IWebApiHost AddComponents<TComponents>()
        {
            windsorRegistrationActions.Add(container =>
            {
                container.Register(
                    Classes.FromAssemblyContaining<TComponents>().Pick().WithService.AllInterfaces().LifestyleTransient()
                );

                Dictionary<Type, MethodInfo> map = new Dictionary<Type, MethodInfo>();

                foreach (var type in typeof(TComponents).Assembly.GetTypes())
                {
                    if (type.Name.EndsWith("Handler"))
                    {
                        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                        foreach (var method in methods)
                        {
                            var firstParameter = method.GetParameters().FirstOrDefault();

                            if (firstParameter != null && firstParameter.ParameterType.Name.EndsWith("Command"))
                            {
                                map.Add(firstParameter.ParameterType, method);
                            }
                        }
                    }
                }

                var handlerMap = new HandlerMap(map);
                container.Register(Component.For<IHandlerMap>().Instance(handlerMap));
            });

            return this;
        }

        public IWebApiHost AddDbContext<TDbContext>() 
            where TDbContext : DbContext
        {
            windsorRegistrationActions.Add(container =>
            {
                container.Register(
                    Classes.FromAssemblyContaining<TDbContext>().Pick().WithService.AllInterfaces().LifestyleTransient()
                );
            });

            dbContextRegistrationAction = services =>
            {
                services.AddTransient<IConnectionStringProvider>(sp =>
                {
                    var cfg = sp.GetRequiredService<IConfiguration>();
                    string connectionString = cfg.GetConnectionString("Sql");
                    return new ConnectionStringProvider(connectionString);
                });

                services.AddScoped(sp =>
                {
                    var operationContext = sp.GetRequiredService<IOperationContext>();
                    var transationState = operationContext.State.Get<TransactionScopeState>();

                    var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
                    optionsBuilder.UseSqlServer(transationState.DbContext.Database.GetDbConnection());

                    var db = (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
                    db.Database.UseTransaction(transationState.DbContextTransaction.GetDbTransaction());

                    return db;
                });
            };

            return this;
        }

        public IWebApiHost AddWebStartup<TWebStartup>()
        {
            webStartupType = typeof(TWebStartup);
            return this;
        }

        public void Run()
        {
            LoggingConfiguration nlogConfig = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            nlogConfig.AddTarget("console", consoleTarget);
            var logger = NLogBuilder.ConfigureNLog(nlogConfig).GetCurrentClassLogger();

            try
            {
                CreateHost().Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private IHostBuilder CreateHost()
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseWindsorContainerServiceProvider()
                .UseNLog()
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                })
                .ConfigureContainer<IWindsorContainer>(container =>
                {
                    container.Install(new FrameworkModuleInstaller<FrameworkComponents>());

                    FrameworkHostConfiguration hostConfig = new FrameworkHostConfiguration();

                    hostConfig.ConfiguredMiddlewares.Add(typeof(OperationNameResolutionMiddleware));
                    hostConfig.ConfiguredMiddlewares.Add(typeof(ValidationMiddleware));
                    hostConfig.ConfiguredMiddlewares.Add(typeof(TransactionScopeMiddleware));

                    container.Register(Component.For<IFrameworkHostConfiguration>().Instance(hostConfig));

                    container.Register(Component.For<TransactionScopeMiddleware>().LifestyleTransient());

                    foreach (var action in windsorRegistrationActions)
                    {
                        action(container);
                    }
                })
                .ConfigureServices(services =>
                {
                    ConfigureDbContext(services);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (webStartupType != null)
                    {
                        webBuilder.UseStartup(webStartupType);
                    }
                    else
                    {
                        webBuilder.UseStartup<DefaultStartup>();
                    }
                });

            return hostBuilder;
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            if(dbContextRegistrationAction != null)
            {
                dbContextRegistrationAction(services);
            }
        }
    }
}
