
namespace AppForeach.Framework
{
    public class OperationMediator : IOperationMediator
    {
        private readonly IOperationExecutor operationExecutor;

        public OperationMediator(IOperationExecutor operationExecutor)
        {
            this.operationExecutor = operationExecutor;
        }

        public IOperationBuilder Execute<TInput>(TInput input)
        {
            var bag = new Bag();

            var operationInput = bag.Get<OperationExecutionInputConfiguration>();

            operationInput.Input = input;
            operationInput.Executor = operationExecutor;

            return new OperationMediatorOperationBuilder(bag);
        }
    }
}
