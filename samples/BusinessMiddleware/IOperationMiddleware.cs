using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessMiddleware
{
    public interface IOperationMiddleware
    {
        Task ExecuteAsync(IOperationContext context, NextOperationDelegate next);
    }
}
