
namespace AppForeach.Framework
{
    public interface IOperationMediator
    {
        IOperationBuilder Execute<TInput>(TInput input);
    }
}
