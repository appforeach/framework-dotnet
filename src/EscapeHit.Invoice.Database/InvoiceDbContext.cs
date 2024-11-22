using System.Threading.Tasks;
using AppForeach.Framework.DependencyInjection;
using EscapeHit.Invoice.Database.Configuration;
using EscapeHit.Invoice.Specification;
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

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new InvoiceEntityConfiguration());
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
