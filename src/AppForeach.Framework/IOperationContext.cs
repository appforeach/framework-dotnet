
namespace AppForeach.Framework
{
    public interface IOperationContext
    {
        string OperationName { get; }

        bool IsCommand { get; }

        object Input { get; }

        FacetBag Configuration { get; }

        Bag State { get; }
    }
}
