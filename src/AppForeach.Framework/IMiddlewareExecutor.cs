using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IMiddlewareExecutor
    {
        Task<OperationOutputState> Execute(OperationContextState state, List<Type> middlewares);
    }
}
