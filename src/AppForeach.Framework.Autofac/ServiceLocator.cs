﻿using AppForeach.Framework.DependencyInjection;
using Autofac;
using System;

namespace AppForeach.Framework.Autofac
{
    public class ServiceLocator : IServiceLocator
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
