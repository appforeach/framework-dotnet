using AppForeach.Framework.DependencyInjection;
using Autofac;
using Autofac.Builder;

namespace AppForeach.Framework.Autofac
{
    public static class ContainerBuilderRegistrationExtensions
    {
        public static void RegisterFrameworkModule<TModule>(this ContainerBuilder containerBuilder)
            where TModule: IFrameworkModule, new()
        {
            RegisterContainerSpecificServices(containerBuilder);

            var module = new TModule();

            foreach (var componentDefinition in module.Components)
            {
                RegisterComponent(containerBuilder, componentDefinition);
            }
        }

        private static void RegisterContainerSpecificServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ServiceLocator>().As<IServiceLocator>()
                .IfNotRegistered(typeof(IServiceLocator));
            containerBuilder.RegisterType<ScopedExecutor>().As<IScopedExecutor>()
                .IfNotRegistered(typeof(IScopedExecutor));
        }

        private static void RegisterComponent(ContainerBuilder containerBuilder, ComponentDefinition componentDefinition)
        {
            if(componentDefinition.ImplementationType != null)
            {
                if (componentDefinition.ImplementationType.IsGenericType && componentDefinition.ImplementationType.ContainsGenericParameters)
                {
                    containerBuilder.RegisterGeneric(componentDefinition.ImplementationType).ApplyServiceAndLifetime(componentDefinition);
                }
                else
                {
                    containerBuilder.RegisterType(componentDefinition.ImplementationType).ApplyServiceAndLifetime(componentDefinition);
                }
            }
            else if(componentDefinition.ImplementationFunction != null)
            {
                containerBuilder.Register(compContext =>
                {
                    var serviceLocator = compContext.Resolve<IServiceLocator>();
                    return componentDefinition.ImplementationFunction(serviceLocator);
                }).ApplyServiceAndLifetime(componentDefinition);
            }
            else if(componentDefinition.ImplementationInstance != null)
            {
                containerBuilder.RegisterInstance(componentDefinition.ImplementationInstance).ApplyServiceAndLifetime(componentDefinition);
            }
            else
            {
                throw new FrameworkException("Undefined component implementation");
            }
        }

        private static void ApplyServiceAndLifetime<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder, ComponentDefinition componentDefinition)
        {
            builder = builder.As(componentDefinition.ComponentType);

            switch (componentDefinition.Lifetime)
            {
                case ComponentLifetime.Transient:
                    builder = builder.InstancePerDependency();
                    break;
                case ComponentLifetime.Scoped:
                    builder = builder.InstancePerLifetimeScope();
                    break;
                case ComponentLifetime.Singleton:
                    builder = builder.SingleInstance();
                    break;
            }

            if (componentDefinition.IsOptional)
            {
                builder.IfNotRegistered(componentDefinition.ComponentType);
            }
        }
    }
}
