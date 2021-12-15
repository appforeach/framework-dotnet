using AppForeach.Framework.DataType;

namespace EscapeHit.App.Commands.CreateUser
{
    public class CreateUserValidation : BaseValidation<CreateUserCommand>
    {
        public CreateUserValidation()
        {
            InheritFromMappingAndSpecification();

            Field(e => e.CustomerNumber).IsOptional();
        }
    }
}
