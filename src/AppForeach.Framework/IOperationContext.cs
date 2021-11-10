
namespace AppForeach.Framework
{
    public interface IOperationContext
    {
        string OperationName { get; }

        bool IsCommand { get; }

        object Input { get; set;  }

        IBag Configuration { get; set; }

        IBag State { get; }
    }
}
