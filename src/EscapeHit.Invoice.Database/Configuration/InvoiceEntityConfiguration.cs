
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppForeach.Framework.EntityFrameworkCore.DataType;
using AppForeach.Framework.DataType;

namespace EscapeHit.Invoice.Database.Configuration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        private readonly BaseEntitySpecification<InvoiceEntity> _entitySpecification;

        public InvoiceEntityConfiguration(BaseEntitySpecification<InvoiceEntity> entitySpecification)
        {
            _entitySpecification = entitySpecification;
        }

        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            //materialze FacetBag

            //idea: take _entitySpecification from DI?
            builder.FromEntitySpecification(_entitySpecification);

            builder.Property(e => e.CustomerNumber).IsUnicode(false);
        }
    }
}
