
namespace AppForeach.Framework.Hosting.Features.Tag;

internal class TagFeatureOption : IApplicationFeatureOption
{
    public IApplicationFeatureInstaller Installer => TagFeatureInstaller.Empty;

    public required string Tag { get; set; }
}
