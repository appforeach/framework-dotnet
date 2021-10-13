
namespace EscapeHit.WebApi
{
    public interface IOperationMediator
    {
        IOperationResult Execute<TInput>(TInput input);
    }
}
