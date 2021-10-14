using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessMiddleware
{
    public static class OperationBuilderExtensions
    {
        public static IOperationBuilder OperationName(this IOperationBuilder operationBuider, string operationName)
        {
            return operationBuider;
        }
    }
}
