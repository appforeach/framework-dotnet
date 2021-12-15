using AppForeach.Framework.DataType;

namespace EscapeHit.App.Specification
{
    public class UserEntitySpecification : BaseEntitySpecification<UserEntity>
    {
        public UserEntitySpecification()
        {
            Field(e => e.CustomerNumber).IsRequired().MaxLength(10);

            Field(e => e.Number).IsRequired().Is<NumberDataType>();
        }
    }
}
