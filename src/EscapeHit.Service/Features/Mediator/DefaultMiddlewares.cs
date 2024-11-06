using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework;
using System;
using System.Collections.Generic;

namespace EscapeHit.Service.Features.Mediator
{
    internal class DefaultMiddlewares
    {
        public static List<Type> GetDefault(bool hasDatabase)
        {
            List<Type> middlewares = new();
            middlewares.Add(typeof(OperationNameResolutionMiddleware));
            //middlewares.Add(typeof(ValidationMiddleware));

            if (hasDatabase)
            {
                middlewares.Add(typeof(TransactionScopeMiddleware));
            }

            return middlewares;
        }
    }
}
