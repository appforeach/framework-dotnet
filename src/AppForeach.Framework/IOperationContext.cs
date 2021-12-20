
namespace AppForeach.Framework
{
    public interface IOperationContext : IOperationState
    {
        string OperationName { get; }

        bool IsCommand { get; }

        object Input { get; }

        IBag Configuration { get; }
    }
}
