using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Features.Tag;

internal class TagFeatureInstaller : IApplicationFeatureInstaller
{
    public static readonly IApplicationFeatureInstaller Empty = new TagFeatureInstaller();
    
    public void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
    {
        // does nothing
    }
}
