using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppForeach.Framework.EntityFrameworkCore.DataType
{
    public static class EntityTypeBuilderExtensions
    {
        public static void FromEntitySpecification<T>(this EntityTypeBuilder<T> ext) where T : class { }
    }
}
