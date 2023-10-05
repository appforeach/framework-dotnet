using AppForeach.Framework.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Microsoft.Extensions.DependencyInjection
{
    internal class ServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceLocator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return serviceProvider.GetService(type);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
