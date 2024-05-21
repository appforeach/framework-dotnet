using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    internal class AuditEntityConfiguration : IEntityTypeConfiguration<AuditEntity>
    {
        public void Configure(EntityTypeBuilder<AuditEntity> builder)
        {
            builder.ToTable("Audit");

            builder.Property(e => e.OperationName).IsRequired();

            builder.Property(e => e.OccuredOn).IsRequired();

            builder.Property(e => e.LoggingTraceId).HasMaxLength(500);

            builder.Property(e => e.LoggingTransactionId).HasMaxLength(500);

            builder.Property(e => e.Type).HasMaxLength(500);

            builder.HasIndex(e => e.TransactionId);

            builder.HasIndex(e => e.InputAuditId);
        }
    }
}
