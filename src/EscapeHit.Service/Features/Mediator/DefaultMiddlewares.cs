using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework;
using System;
using System.Collections.Generic;
using AppForeach.Framework.Validation;

namespace EscapeHit.Service.Features.Mediator
{
    internal class DefaultMiddlewares
    {
        public static List<Type> GetDefault(bool hasDatabase)
        {
            List<Type> middlewares = new();
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
