using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public interface IFrameworkHostConfiguration
    {
        List<Type> ConfiguredMiddlewares { get; }

        Action<IOperationBuilder> OperationConfiguration { get; }
    }
}
