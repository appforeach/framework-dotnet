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
            IRegistrationBuilder<object, IConcreteActivatorData, SingleRegistrationStyle> builder;

            if(componentDefinition.ImplementationType != null)
            {
                builder = containerBuilder.RegisterType(componentDefinition.ImplementationType);
            }
            else if(componentDefinition.ImplementationFunction != null)
            {
                builder = containerBuilder.Register(compContext =>
                {
                    var serviceLocator = compContext.Resolve<IServiceLocator>();
                    return componentDefinition.ImplementationFunction(serviceLocator);
                });
            }
            else if(componentDefinition.ImplementationInstance != null)
            {
                builder = containerBuilder.RegisterInstance(componentDefinition.ImplementationInstance);
            }
            else
            {
                throw new FrameworkException("Undefined component implementation");
            }

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

            if(componentDefinition.IsOptional)
            {
                builder.IfNotRegistered(componentDefinition.ComponentType);
            }
        }
    }
}
