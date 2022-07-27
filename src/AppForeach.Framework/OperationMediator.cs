
using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationMediator : IOperationMediator
    {
        private readonly IOperationExecutor operationExecutor;

        public OperationMediator(IOperationExecutor operationExecutor)
        {
            this.operationExecutor = operationExecutor;
        }

        public Task<OperationResult> Execute<TInput>(TInput input, Action<IOperationBuilder> options = null)
            => operationExecutor.Execute(input, options);
    }
}
