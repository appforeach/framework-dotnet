using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IOperationExecutor
    {
        Task<OperationResult> Execute(IBag input);
    }
}
