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

            containerBuilder.RegisterType<OperationNameResolutionMiddleware>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ValidationMiddleware>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<DefaultValidationFailedEventHandler>().As<IValidationFailedEventHandler>().InstancePerLifetimeScope()
                .IfNotRegistered(typeof(IValidationFailedEventHandler));

            containerBuilder.RegisterType<DefaultExceptionEventHandler>().As<IExceptionEventHandler>().InstancePerLifetimeScope()
                .IfNotRegistered(typeof(IExceptionEventHandler));

            containerBuilder.RegisterType<DefaultUnhandledExceptionEventHandler>().As<IUnhandledExceptionEventHandler>().InstancePerLifetimeScope()
                .IfNotRegistered(typeof(IUnhandledExceptionEventHandler));
            
        }
    }
}
