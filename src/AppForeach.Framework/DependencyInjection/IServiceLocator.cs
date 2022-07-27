using System;

namespace AppForeach.Framework.DependencyInjection
{
    public interface IServiceLocator
    {
        T GetService<T>();

        object GetService(Type type);
    }
}
