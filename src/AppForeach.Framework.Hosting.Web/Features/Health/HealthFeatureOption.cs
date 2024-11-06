using AppForeach.Framework.Hosting.Features;

namespace AppForeach.Framework.Hosting.Web.Features.Health;

internal class HealthFeatureOption : IApplicationFeatureOption
{
    public IApplicationFeatureInstaller Installer => new HealthFeatureInstaller();
}
