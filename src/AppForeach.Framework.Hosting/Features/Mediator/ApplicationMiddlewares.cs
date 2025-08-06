using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework.Logging;
using AppForeach.Framework.Validation;
using System;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Mediator
{
    internal static class ApplicationMiddlewares
    {
        public static List<Type> GetDefaultMiddlewares(bool hasDatabase)
        {
            List<Type> middlewares = new();
            middlewares.Add(typeof(OperationLoggingMiddleware));
            middlewares.Add(typeof(ExceptionHandlerMiddleware));
            middlewares.Add(typeof(OperationNameResolutionMiddleware));
            middlewares.Add(typeof(ValidationMiddleware));

            if (hasDatabase)
            {
                middlewares.Add(typeof(TransactionScopeMiddleware));
            }

            return middlewares;
        }
    }
}
