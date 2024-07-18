using AppForeach.Framework.Hosting.Features;
using Microsoft.AspNetCore.Builder;

namespace AppForeach.Framework.Hosting.Web.Features
{
    public class FrameworkWebApplicationFeatureInstaller : FrameworkApplicationFeatureInstaller
    {
        public FrameworkWebApplicationFeatureInstaller(WebApplicationBuilder webApplicationBuilder)
            : base(webApplicationBuilder.Services, webApplicationBuilder.Configuration)
        {
        }

        public void SetUpWeb(WebApplication webApp)
        {
            var webInstallers = FeatureInstallers.OfType<IWebApplicationFeatureInstaller>();

            foreach (var installer in webInstallers)
            {
                installer.SetUpWeb(InstallContext, webApp);
            }
        }
    }
}
