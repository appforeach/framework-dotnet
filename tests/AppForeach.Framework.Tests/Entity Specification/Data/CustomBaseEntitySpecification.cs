using AppForeach.Framework.DataType;

namespace AppForeach.Framework.Tests.Entity_Specification.Data;

internal class CustomBaseEntitySpecification<TEntity> : BaseEntitySpecification<TEntity>
{
    public CustomBaseEntitySpecification()
    {
        Type<string>().HasMaxLength(50);
        Type<decimal>().HasPrecision(4, 2);
    }
}
