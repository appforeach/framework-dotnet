using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace AppForeach.Framework.Hosting.Features
{
    public class FrameworkApplicationFeatureInstaller
    {
        protected IServiceCollection Services { get; private set; }

        protected IConfigurationManager Configuration { get; private set; }

        protected IList<IApplicationFeatureOption> FeatureOptions { get; private set; }

        protected IList<IApplicationFeatureInstaller> FeatureInstallers { get; private set; }

        protected IApplicationFeatureInstallContext InstallContext { get; private set; }

        public FrameworkApplicationFeatureInstaller(IServiceCollection services, IConfigurationManager configuration)
        {
            Services = services;
            Configuration = configuration;

            FeatureOptions = Services
                .Where(sd => sd.ImplementationInstance is IApplicationFeatureOption)
                .Select(sd => (IApplicationFeatureOption)sd.ImplementationInstance!)
                .ToList();

            FeatureInstallers = FeatureOptions
                .Select(f => f.Installer)
                .Distinct()
                .ToList();

            InstallContext = new ApplicationFeatureInstallContext
            {
                Configuration = Configuration,
                Options = FeatureOptions
            };
        }

        public void SetUpServices(IServiceCollection services)
        {
            foreach (var installer in FeatureInstallers)
            {
                installer.SetUpServices(InstallContext, services);
            }
        }
    }
}
