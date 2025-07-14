
using System;
using System.Threading;
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


        public Task<OperationResult> Execute<TInput>(TInput input, Action<IOperationBuilder> options = null, CancellationToken cancellationToken = default)
            => operationExecutor.Execute(input, options, cancellationToken);

        public Task<OperationResult> Execute<TInput>(TInput input, CancellationToken cancellationToken)
            => operationExecutor.Execute(input, null, cancellationToken);
    }
}
