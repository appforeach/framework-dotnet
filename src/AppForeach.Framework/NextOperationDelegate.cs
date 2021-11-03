using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public delegate Task NextOperationDelegate(IOperationContext context);
}
