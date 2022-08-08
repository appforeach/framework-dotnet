using AppForeach.Framework.DependencyInjection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;

namespace AppForeach.Framework.Castle.Windsor
{
    public class FrameworkModuleInstaller<TFrameworkModule> : IWindsorInstaller
        where TFrameworkModule : IFrameworkModule, new()
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallContainerSpecificServices(container);

            var module = new TFrameworkModule();

            foreach (var componentDefinition in module.Components)
            {
                InstallComponent(container, componentDefinition);
            }
        }

        private void InstallContainerSpecificServices(IWindsorContainer container)
        {
            container.Register(Component.For<IServiceLocator>().ImplementedBy<ServiceLocator>().OnlyNewServices());
        }

        private void InstallComponent(IWindsorContainer container, ComponentDefinition componentDefinition)
        {
            ComponentRegistration registration = Component.For(componentDefinition.ComponentType);
            ComponentRegistration<object> objectRegistration;

            if (componentDefinition.ImplementationType != null)
            {
                objectRegistration = registration.ImplementedBy(componentDefinition.ImplementationType);
            }
            else if (componentDefinition.ImplementationFunction != null)
            {
                objectRegistration = registration.UsingFactoryMethod(kernel =>
                {
                    var serviceLocator = kernel.Resolve<IServiceLocator>();
                    return componentDefinition.ImplementationFunction(serviceLocator);
                });
            }
            else if (componentDefinition.ImplementationInstance != null)
            {
                objectRegistration = registration.Instance(componentDefinition.ImplementationInstance);
            }
            else
            {
                throw new FrameworkException("Undefined component implementation");
            }

            switch (componentDefinition.Lifetime)
            {
                case ComponentLifetime.Transient:
                    objectRegistration = objectRegistration.LifestyleTransient();
                    break;
                case ComponentLifetime.Scoped:
                    objectRegistration = objectRegistration.LifeStyle.ScopedToNetServiceScope();
                    break;
                case ComponentLifetime.Singleton:
                    objectRegistration = objectRegistration.LifestyleSingleton();
                    break;
            }

            if (componentDefinition.IsOptional)
            {
                objectRegistration = objectRegistration.OnlyNewServices();
            }

            container.Register(objectRegistration);
        }
    }
}
