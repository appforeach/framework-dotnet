
namespace AppForeach.Framework
{
    public class OperationState : IOperationState
    {
        public OperationState()
        {
            State = new Bag();
        }

        public IBag State { get; }
    }
}
