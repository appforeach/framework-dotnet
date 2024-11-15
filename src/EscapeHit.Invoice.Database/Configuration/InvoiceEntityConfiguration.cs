
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppForeach.Framework.EntityFrameworkCore.DataType;

namespace EscapeHit.Invoice.Database.Configuration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            //materialze FacetBag

            builder.FromEntitySpecification();

            builder.Property(e => e.CustomerNumber).IsUnicode(false);
        }
    }
}
