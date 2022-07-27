using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IOperationExecutor
    {
        Task<OperationResult> Execute(object input, Action<IOperationBuilder> options);
    }
}
