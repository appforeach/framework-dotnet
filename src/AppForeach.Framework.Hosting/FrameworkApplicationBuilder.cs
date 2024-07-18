using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting
{
    public class FrameworkApplicationBuilder
    {
        protected readonly string[] args;
        protected readonly List<Action<IServiceCollection, IConfiguration>> configureServicesActions = new List<Action<IServiceCollection, IConfiguration>>();

        public FrameworkApplicationBuilder(string[] args)
        {
            this.args = args;
        }

        public void ConfigureServices(Action<IServiceCollection> configureServices)
        {
            configureServicesActions.Add((services, config) => configureServices(services));
        }

        public void ConfigureServices(Action<IServiceCollection, IConfiguration> configureServices)
        {
            configureServicesActions.Add(configureServices);
        }

        public virtual void Run()
        {
        }

        public virtual void RunApp()
        {

        }
    }
}
