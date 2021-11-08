using System.Threading.Tasks;
using EscapeHit.Invoice.Repositories;

namespace EscapeHit.Invoice.Database.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext db;

        public InvoiceRepository(InvoiceDbContext db)
        {
            this.db = db;
        }

        public async Task Create(InvoiceEntity invoice)
        {
            db.Add(invoice);
            await db.SaveChangesAsync();
        }

        public Task<InvoiceEntity> FindById(int id)
        {
            return Task.FromResult(new InvoiceEntity());
        }
    }
}
