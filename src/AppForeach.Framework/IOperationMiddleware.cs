using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IOperationMiddleware
    {
        Task ExecuteAsync(IOperationContext context, NextOperationDelegate next);
    }
}
