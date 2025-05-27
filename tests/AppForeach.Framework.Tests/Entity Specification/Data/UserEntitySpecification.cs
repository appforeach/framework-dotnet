using AppForeach.Framework.DataType;
namespace AppForeach.Framework.Tests.Entity_Specification.Data;

internal class UserEntitySpecification : CustomBaseEntitySpecification<UserEntity>
{
    public UserEntitySpecification()
    {
        Field(x => x.LastName).MaxLength(20);
    }
}
