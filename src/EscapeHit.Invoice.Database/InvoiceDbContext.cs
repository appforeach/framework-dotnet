using System.Threading.Tasks;
using AppForeach.Framework.DependencyInjection;
using EscapeHit.Invoice.Database.Configuration;
using EscapeHit.Invoice.Specification;
using Microsoft.EntityFrameworkCore;

namespace EscapeHit.Invoice.Database
{
    public class InvoiceDbContext : DbContext
    {
        private readonly IServiceLocator _serviceLocator;
        public InvoiceDbContext()
        {
        }

        public InvoiceDbContext(DbContextOptions options, IServiceLocator serviceLocator) : base(options)
        {
            _serviceLocator = serviceLocator;
        }

        public DbSet<InvoiceEntity> Invoices { get; set; }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var invoiceEntitySpecification = _serviceLocator.GetService<InvoiceEntitySpecification>();
            
            modelBuilder.ApplyConfiguration(new InvoiceEntityConfiguration(invoiceEntitySpecification));
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
