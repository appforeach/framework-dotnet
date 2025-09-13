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

        public async Task<InvoiceEntity> FindById(int id)
        {
            return await db.Invoices.FindAsync(id);
        }
    }
}
