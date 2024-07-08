using System;
using System.Threading.Tasks;

namespace AppForeach.Framework.DependencyInjection
{
    public interface IScopedExecutor
    {
        Task<TResult> Execute<TService, TResult>(Func<TService, Task<TResult>> executeFunction, bool transferState);
    }
}
