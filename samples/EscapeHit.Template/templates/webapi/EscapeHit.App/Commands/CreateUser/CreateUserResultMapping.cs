
namespace EscapeHit.App.Commands.CreateUser
{
    public interface ICreateUserOutputMapping
    {
        CreateUserResult MapFrom(UserEntity entity);
    }

    public class CreateUserResultMapping : ICreateUserOutputMapping
    {
        public CreateUserResult MapFrom(UserEntity entity)
        {
            var output = new CreateUserResult();
            
            output.UserId = entity.Id;
            output.UserNumber = entity.Number;
            
            return output;
        }
    }
}
