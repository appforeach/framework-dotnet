using Microsoft.EntityFrameworkCore;

namespace EscapeHit.Invoice.Database
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext()
        {
        }

        public InvoiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<InvoiceEntity> Invoices { get; set; }
    }
}
