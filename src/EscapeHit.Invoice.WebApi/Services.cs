using EscapeHit.Invoice.Database;
using Microsoft.Extensions.DependencyInjection;

namespace EscapeHit.Invoice.WebApi
{
    public class Services
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddEscapeHitMediator();

            services.AddEscapeHitBusiness<InvoiceComponents>();

            services.AddEscapeHitSql<InvoiceDbContext>();
        }
    }
}
