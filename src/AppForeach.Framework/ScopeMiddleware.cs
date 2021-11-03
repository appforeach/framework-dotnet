using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class ScopeMiddleware : IOperationMiddleware
    {
        public ScopeMiddleware()
        {
        }

        public async Task ExecuteAsync(IOperationContext context, NextOperationDelegate next)
        {
            await next(context);
        }
    }
}
