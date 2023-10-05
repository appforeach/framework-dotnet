
namespace AppForeach.Framework
{
    public class OperationStateProvider : IOperationStateProvider
    {
        public OperationStateProvider()
        {
            State = new Bag();
        }

        public Bag State { get; set; }
    }
}
