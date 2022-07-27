using System;
using AppForeach.Framework.DependencyInjection;
using Castle.MicroKernel;

namespace AppForeach.Framework.Castle.Windsor
{
    internal class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public T GetService<T>()
        {
            return (T)kernel.Resolve(typeof(T));
        }

        public object GetService(Type type)
        {
            return kernel.Resolve(type);
        }
    }
}
