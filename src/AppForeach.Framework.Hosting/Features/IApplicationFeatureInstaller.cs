using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Features
{
    public interface IApplicationFeatureInstaller
    {
        void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services);
    }
}
