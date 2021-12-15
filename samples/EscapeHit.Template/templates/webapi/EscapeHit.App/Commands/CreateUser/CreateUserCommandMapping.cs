
using AppForeach.Framework.DataType;

namespace EscapeHit.App.Commands.CreateUser
{
    public interface ICreateUserInputMapping
    {
        UserEntity MapFrom(CreateUserCommand input);
    }

    public class CreateUserCommandMapping : BaseMapping<UserEntity, CreateUserCommand>, ICreateUserInputMapping
    {
        public CreateUserCommandMapping()
        {
            Field(e => e.CustomerNumber).From(e => e.CustomerNumber);
        }

        public UserEntity MapFrom(CreateUserCommand input) => BaseMap(input);
    }
}
