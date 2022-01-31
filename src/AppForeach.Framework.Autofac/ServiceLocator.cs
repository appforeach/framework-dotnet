using AppForeach.Framework;
using Autofac;
using System;

namespace AppForeach.Framework.Autofac
{
    internal class ServiceLocator : IServiceLocator
    {
        private readonly IComponentContext componentContext;

        public ServiceLocator(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public T GetService<T>()
        {
            return componentContext.Resolve<T>();
        }

        public object GetService(Type type)
        {
            return componentContext.Resolve(type);
        }
    }
}
