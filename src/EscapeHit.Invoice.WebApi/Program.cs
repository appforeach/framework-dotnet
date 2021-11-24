using EscapeHit.Invoice.Database;
using EscapeHit.WebApi;

namespace EscapeHit.Invoice.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApiHost.Create(args)
                .AddComponents<InvoiceComponents>()
                .AddDbContext<InvoiceDbContext>()
                .Run();
        }
    }
}
