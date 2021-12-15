
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppForeach.Framework.EntityFrameworkCore.DataType;

namespace EscapeHit.App.Database.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.FromEntitySpecification();

            builder.Property(e => e.CustomerNumber).IsUnicode(false);
        }
    }
}
