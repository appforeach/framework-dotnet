using EscapeHit.App.Database;
using EscapeHit.WebApi;

namespace EscapeHit.App.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApiHost.Create(args)
                .AddComponents<AppComponents>()
                .AddDbContext<AppDbContext>()
                .Run();
        }
    }
}
