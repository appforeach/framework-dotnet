using AppForeach.Framework.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppForeach.Framework.Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionModuleExtensions
    {
        public static void AddFrameworkModule<TModule>(this IServiceCollection serviceCollection)
            where TModule : IFrameworkModule, new()
        {
            AddSpecificServices(serviceCollection);

            var module = new TModule();

            foreach (var componentDefinition in module.Components)
            {
                AddComponent(serviceCollection, componentDefinition);
            }
        }

        private static void AddComponent(IServiceCollection serviceCollection, ComponentDefinition componentDefinition)
        {
            var serviceDescriptor = MapToServiceDescriptor(componentDefinition);
            
            if(componentDefinition.IsOptional)
            {
                serviceCollection.TryAdd(serviceDescriptor);
            }
            else
            {
                serviceCollection.Add(serviceDescriptor);
            }
        }

        private static ServiceDescriptor MapToServiceDescriptor(ComponentDefinition componentDefinition)
        {
            ServiceDescriptor serviceDescriptor;

            if (componentDefinition.ImplementationInstance != null) 
            {
                serviceDescriptor = new ServiceDescriptor(componentDefinition.ComponentType, componentDefinition.ImplementationInstance);
            }
            else if(componentDefinition.ImplementationFunction != null)
            {
                serviceDescriptor = new ServiceDescriptor(componentDefinition.ComponentType, (sp) =>
                {
                    var serviceLocator = sp.GetService<IServiceLocator>();
                    return componentDefinition.ImplementationFunction(serviceLocator);
                }, MapServiceLifetime(componentDefinition.Lifetime));
            }
            else if(componentDefinition.ImplementationType != null)
            {
                serviceDescriptor = new ServiceDescriptor(componentDefinition.ComponentType, componentDefinition.ImplementationType,
                    MapServiceLifetime(componentDefinition.Lifetime));
            }
            else
            {
                throw new FrameworkException("ComponentDefinition does not have any implementation.");
            }

            return serviceDescriptor;
        }

        private static ServiceLifetime MapServiceLifetime(ComponentLifetime componentLifetime)
        {
            switch (componentLifetime)
            {
                case ComponentLifetime.Transient:
                    return ServiceLifetime.Transient;
                case ComponentLifetime.Scoped:
                    return ServiceLifetime.Scoped;
                case ComponentLifetime.Singleton:
                    return ServiceLifetime.Singleton;
                default:
                    throw new FrameworkException("Unsupported ComponentLifetime.");
            }
        }

        private static void AddSpecificServices(IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<IScopedExecutor, ScopedExecutor>();
            serviceCollection.TryAddScoped<IServiceLocator, ServiceLocator>();
        }
    }
}
