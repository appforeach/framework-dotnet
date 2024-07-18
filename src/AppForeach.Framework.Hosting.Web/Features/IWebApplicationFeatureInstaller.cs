using AppForeach.Framework.Hosting.Features;
using Microsoft.AspNetCore.Builder;

namespace AppForeach.Framework.Hosting.Web.Features
{
    public interface IWebApplicationFeatureInstaller : IApplicationFeatureInstaller
    {
        void SetUpWeb(IApplicationFeatureInstallContext installContext, WebApplication web);
    }
}
