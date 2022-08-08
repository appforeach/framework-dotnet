using AppForeach.Framework.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EscapeHit.WebApi
{
    public interface IWebApiHost
    {
        IWebApiHost AddComponents<TComponents>()
            where TComponents : IFrameworkModule, new();

        IWebApiHost AddDbContext<TDbContext>()
            where TDbContext : DbContext;

        IWebApiHost AddWebStartup<TWebStartup>();

        void Run();
    }
}
