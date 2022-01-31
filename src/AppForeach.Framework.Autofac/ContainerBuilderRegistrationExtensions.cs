using AppForeach.Framework;
using Autofac;

namespace AppForeach.Framework.Autofac
{
    public static class ContainerBuilderRegistrationExtensions
    {
        public static void RegisterFramework(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ServiceLocator>().As<IServiceLocator>();

            containerBuilder.RegisterType<HandlerInvoker>().As<IHandlerInvoker>();

            containerBuilder.RegisterType<OperationMediator>().As<IOperationMediator>();

            containerBuilder.RegisterType<HandlerInvokerMiddleware>().As<IHandlerInvokerMiddleware>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<OperationExecutor>().As<IOperationExecutor>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<OperationState>().As<IOperationState>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<OperationContext>().As<IOperationContext>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<OperationNameResolver>().As<IOperationNameResolver>();

            containerBuilder.RegisterType<OperationNameResolutionMiddleware>().As<IOperationNameResolutionMiddleware>().InstancePerLifetimeScope();
        }
    }
}
