using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppForeach.Framework.EntityFrameworkCore
{
    internal class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.OccuredOn).IsRequired();
        }
    }
}
