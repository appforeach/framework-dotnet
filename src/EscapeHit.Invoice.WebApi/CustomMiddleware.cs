using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppForeach.Framework;
using Microsoft.Extensions.Logging;

namespace EscapeHit.Invoice.WebApi
{
    public class CustomMiddleware : IOperationMiddleware
    {
        private readonly ILogger<CustomMiddleware> logger;

        public CustomMiddleware(ILogger<CustomMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task ExecuteAsync(IOperationContext context, NextOperationDelegate next)
        {
            logger.LogWarning("CustomMiddleware - before");
            await next(context);
            logger.LogWarning("CustomMiddleware - after");
        }
    }
}
