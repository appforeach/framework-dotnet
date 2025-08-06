using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EscapeHit.Invoice.Database.Design
{
    public class InvoiceDbContextDesignFactory : IDesignTimeDbContextFactory<InvoiceDbContext>
    {
        public InvoiceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InvoiceDbContext>();
            optionsBuilder.UseSqlServer("Server=(local); Initial Catalog=invoice; Integrated Security=true; MultipleActiveResultSets=True;Trust Server Certificate=True;");
            return new InvoiceDbContext(optionsBuilder.Options);
        }
    }
}
