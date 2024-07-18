using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using AppForeach.Framework.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.Mediator
{
    public class MediatorFeatureInstaller : IApplicationFeatureInstaller
    {
        protected readonly MediatorFeatureOption option;

        public MediatorFeatureInstaller(MediatorFeatureOption option)
        {
            this.option = option;
        }

        public void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
        {
            bool hasDatabase = installContext.Options.OfType<ISqlFeatureOption>().Any();

            services.AddFrameworkModule<FrameworkComponents>();

            services.AddSingleton<IFrameworkHostConfiguration>(sp => GetHostConfiguration(hasDatabase)); 
        }

        protected virtual FrameworkHostConfiguration GetHostConfiguration(bool hasDatabase) 
        {
            FrameworkHostConfiguration hostConfig = new FrameworkHostConfiguration();

            hostConfig.ConfiguredMiddlewares.AddRange(option.Middlewares ?? GetDefaultMiddlewares(hasDatabase));

            hostConfig.OperationConfiguration = (opt) =>
            {
                option.ApplicationOptions?.Invoke(opt);
            };

            return hostConfig;
        }

        protected virtual List<Type> GetDefaultMiddlewares(bool hasDatabase)
        {
            List<Type> middlewares = new();
            middlewares.Add(typeof(OperationNameResolutionMiddleware));
            middlewares.Add(typeof(ValidationMiddleware));

            if (hasDatabase)
            {
                middlewares.Add(typeof(TransactionScopeMiddleware));
            }

            return middlewares;
        }
    }
}
