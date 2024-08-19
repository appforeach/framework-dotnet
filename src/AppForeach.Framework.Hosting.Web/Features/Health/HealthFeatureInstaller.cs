using AppForeach.Framework.Hosting.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Web.Features.Health;

internal class HealthFeatureInstaller : IWebApplicationFeatureInstaller
{
    public void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
    {
    }

    public void SetUpWeb(IApplicationFeatureInstallContext installContext, WebApplication web)
    {
        web.UseWhen(
            context => context.Request.Path.StartsWithSegments("/health"),
            appBuilder =>
            {
                appBuilder.Run(async (context) =>
                {
                    context.Response.Headers["Content-Type"] = new string[] { "text/plain" };
                    context.Response.Headers["Cache-Control"] = new string[] { "no-cache, no-store, must-revalidate" };
                    context.Response.Headers["Pragma"] = new string[] { "no-cache" };
                    context.Response.Headers["Expires"] = new string[] { "0" };
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("ok");
                });
            });
    }
}
