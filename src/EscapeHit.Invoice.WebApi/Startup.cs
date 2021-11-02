using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppForeach.Framework;
using AppForeach.Framework.Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EscapeHit.Invoice.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EscapeHit.Invoice.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(IWindsorContainer container)
        {
            container.Install(new FrameworkInstaller());

            container.Register(
                Classes.FromAssemblyContaining<InvoiceEntity>().Pick().WithService.AllInterfaces().LifestyleTransient()
            );

            container.Register(
                Classes.FromAssemblyContaining<InvoiceDatabaseContext>().Pick().WithService.AllInterfaces().LifestyleTransient()
            );

            container.Register(
                Classes.FromAssembly(typeof(Startup).Assembly).Pick().WithService.AllInterfaces().LifestyleTransient()
            );

            RegisterHandlers(container, typeof(InvoiceEntity).Assembly);
        }

        private void RegisterHandlers(IWindsorContainer container, Assembly assembly)
        {
            Dictionary<Type, MethodInfo> map = new Dictionary<Type, MethodInfo>();
            
            foreach(var type in assembly.GetTypes())
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
        }

        private static bool IsCommandHandler(Type t)
        {
            return GetCommandHandlerCommandType(t) != null;
        }

        private static Type GetCommandHandlerCommandType(Type type)
        {
            Type commandType = null;



            return commandType;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IUseCase, UseCase>();
            //services.AddScoped<IScopedService, ScopedService>();
            //services.AddSingleton<ISingletonService, SingletonService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
