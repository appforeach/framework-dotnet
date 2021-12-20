using System;

namespace AppForeach.Framework
{
    public interface IOperationNameResolver
    {
        OperationName ResolveName(Type inputType, Type handlerType);
    }
}
