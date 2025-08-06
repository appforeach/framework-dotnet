using Microsoft.AspNetCore.Builder;
using Serilog;

namespace EscapeHit.WebApi
{
    internal static class Web
    {
        public static void Configure(WebApplication app)
        {
            app.UseSerilogRequestLogging();
            app.MapControllers();
        }
    }
}
