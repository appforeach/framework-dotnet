using EscapeHit.WebApi;

namespace EscapeHit.Invoice.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = EscapeHitWebApplication.CreateBuilder(args);

        builder.ConfigureServices(Services.Setup);

        builder.Run();
    }
}
