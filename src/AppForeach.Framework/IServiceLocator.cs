using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework
{
    public interface IServiceLocator
    {
        T GetService<T>();

        object GetService(Type type);
    }
}
