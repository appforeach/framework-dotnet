using System;
using System.Collections.Generic;

namespace AppForeach.Framework.DependencyInjection
{
    public class FrameworkModule : IFrameworkModule
    {
        private readonly List<ComponentDefinition> componentDefinitions = new List<ComponentDefinition>();

        public IEnumerable<ComponentDefinition> Components => componentDefinitions;

        public ComponentLifetime? AssemblyDefaultLifetime { get; set; }

        public void Component(Type componentType, Type implementationType, ComponentLifetime componentLifetime, bool isOptional = false)
        {
            componentDefinitions.Add(new ComponentDefinition
            {
                ComponentType = componentType,
                Lifetime = componentLifetime,
                ImplementationType = implementationType,
                IsOptional = isOptional
            });
        }

        public void Component(Type componentType, object implementationInstance, ComponentLifetime componentLifetime, bool isOptional = false)
        {
            componentDefinitions.Add(new ComponentDefinition
            {
                ComponentType = componentType,
                Lifetime = componentLifetime,
                ImplementationInstance = implementationInstance,
                IsOptional = isOptional
            });
        }

        public void Component(Type componentType, Func<IServiceLocator, object> implementationFunction, ComponentLifetime componentLifetime, bool isOptional = false)
        {
            componentDefinitions.Add(new ComponentDefinition
            {
                ComponentType = componentType,
                Lifetime = componentLifetime,
                ImplementationFunction = implementationFunction,
                IsOptional = isOptional
            });
        }

        public void Component<TComponent, TImplementation>(ComponentLifetime componentLifetime, bool isOptional = false)
            => Component(typeof(TComponent), typeof(TImplementation), componentLifetime, isOptional);

        public void Transient<TComponent, TImplementation>(bool isOptional = false)
            => Component<TComponent, TImplementation>(ComponentLifetime.Transient, isOptional);

        public void Scoped<TComponent, TImplementation>(bool isOptional = false)
            => Component<TComponent, TImplementation>(ComponentLifetime.Scoped, isOptional);

        public void Singleton<TComponent, TImplementation>(bool isOptional = false)
            => Component<TComponent, TImplementation>(ComponentLifetime.Singleton, isOptional);

        public void Singleton<TComponent>(TComponent instance, bool isOptional = false)
            => Component(typeof(TComponent), instance, ComponentLifetime.Singleton, isOptional);

        public void AssemblyDefaultLifetimeTransient()
        {
            AssemblyDefaultLifetime = ComponentLifetime.Transient;
        }

        public void AssemblyDefaultLifetimeScoped()
        {
            AssemblyDefaultLifetime = ComponentLifetime.Scoped;
        }

        public void AssemblyDefaultLifetimeSingleton()
        {
            AssemblyDefaultLifetime = ComponentLifetime.Singleton;
        }

        public void AssemblyNoDefaultRegistration()
        {
            AssemblyDefaultLifetime = null;
        }
    }
}
