
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IOperationMediator
    {
        Task<OperationResult> Execute<TInput>(TInput input, Action<IOperationBuilder> options = null, CancellationToken cancellationToken = default);

        Task<OperationResult> Execute<TInput>(TInput input, CancellationToken cancellationToken);
    }
}
