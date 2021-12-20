using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IHandlerInvoker
    {
        Task<object> Invoke(object operationInput);
    }
}
