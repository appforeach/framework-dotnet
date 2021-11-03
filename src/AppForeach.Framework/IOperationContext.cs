
namespace AppForeach.Framework
{
    public interface IOperationContext
    {
        string OperationName { get; }

        bool IsCommand { get; }

        object Input { get; }

        IBag Configuration { get; }

        IBag State { get; }
    }
}
