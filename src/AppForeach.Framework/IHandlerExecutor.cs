using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IHandlerExecutor
    {
        Task<object> Execute(object operationInput);
    }
}
