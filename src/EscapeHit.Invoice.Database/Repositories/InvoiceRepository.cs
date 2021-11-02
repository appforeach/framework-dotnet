using System.Threading.Tasks;
using EscapeHit.Invoice.Repositories;

namespace EscapeHit.Invoice.Database.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public Task Create(InvoiceEntity invoice)
        {
            return Task.CompletedTask;
        }

        public Task<InvoiceEntity> FindById(int id)
        {
            return Task.FromResult(new InvoiceEntity());
        }
    }
}
