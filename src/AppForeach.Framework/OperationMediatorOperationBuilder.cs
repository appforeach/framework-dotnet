
namespace AppForeach.Framework
{
    public class OperationMediatorOperationBuilder : IOperationBuilder
    {
        private readonly IBag bag;

        public OperationMediatorOperationBuilder(IBag bag)
        {
            this.bag = bag;
        }

        public IBag Configuration => bag;
    }
}
