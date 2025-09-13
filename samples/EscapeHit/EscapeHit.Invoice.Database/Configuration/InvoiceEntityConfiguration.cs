
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppForeach.Framework.EntityFrameworkCore;

namespace EscapeHit.Invoice.Database.Configuration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            builder.InheritFromEntitySpecification();
            builder.Property(e => e.CustomerNumber).IsUnicode(false);
        }
    }
}
