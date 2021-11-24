using Microsoft.EntityFrameworkCore;

namespace EscapeHit.WebApi
{
    public interface IWebApiHost
    {
        IWebApiHost AddComponents<TComponents>();

        IWebApiHost AddDbContext<TDbContext>()
            where TDbContext : DbContext;

        IWebApiHost AddWebStartup<TWebStartup>();

        void Run();
    }
}
