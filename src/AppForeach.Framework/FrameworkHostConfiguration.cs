using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class FrameworkHostConfiguration : IFrameworkHostConfiguration
    {
        public FrameworkHostConfiguration()
        {
            ConfiguredMiddlewares = new List<Type>();
        }

        public List<Type> ConfiguredMiddlewares { get; }
    }
}
