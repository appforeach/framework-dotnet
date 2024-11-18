
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppForeach.Framework.EntityFrameworkCore.DataType;
using EscapeHit.Invoice.Specification;

namespace EscapeHit.Invoice.Database.Configuration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        private readonly InvoiceEntitySpecification _entitySpecification;

        public InvoiceEntityConfiguration(InvoiceEntitySpecification entitySpecification)
        {
            _entitySpecification = entitySpecification;
        }

        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            builder.FromEntitySpecification(_entitySpecification);

            builder.Property(e => e.CustomerNumber).IsUnicode(false);
        }
    }
}
