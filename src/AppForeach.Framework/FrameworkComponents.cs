using AppForeach.Framework.DependencyInjection;

namespace AppForeach.Framework
{
    public class FrameworkComponents : FrameworkModule
    {
        public FrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Transient<IOperationMediator, OperationMediator>();

            Scoped<IHandlerInvoker, HandlerInvoker>();
            Scoped<IHandlerInvokerMiddleware, HandlerInvokerMiddleware>();
            Scoped<IOperationExecutor, OperationExecutor>();
            Scoped<IOperationContext, OperationContext>();

            Singleton<IOperationNameResolver, OperationNameResolver>();
            Scoped<OperationNameResolutionMiddleware, OperationNameResolutionMiddleware>();
            Scoped<ValidationMiddleware, ValidationMiddleware>();

            Component(typeof(IValidationFailedEventHandler), typeof(DefaultValidationFailedEventHandler), ComponentLifetime.Scoped, isOptional: true);
            Component(typeof(IExceptionEventHandler), typeof(DefaultExceptionEventHandler), ComponentLifetime.Scoped, isOptional: true);
            Component(typeof(IUnhandledExceptionEventHandler), typeof(DefaultUnhandledExceptionEventHandler), ComponentLifetime.Scoped, isOptional: true);
        }
    }
}
