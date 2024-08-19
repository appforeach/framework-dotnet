using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using System.Linq;

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

            var getMiddlewares = option.GetMiddlewares ?? ApplicationMiddlewares.GetDefaultMiddlewares;
            hostConfig.ConfiguredMiddlewares.AddRange(getMiddlewares(hasDatabase));

            hostConfig.OperationConfiguration = (opt) =>
            {
                option.ApplicationOptions?.Invoke(opt);
            };

            return hostConfig;
        }
    }
}
