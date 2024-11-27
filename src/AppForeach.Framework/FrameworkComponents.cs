using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Logging;

namespace AppForeach.Framework
{
    public class FrameworkComponents : FrameworkModule
    {
        public FrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Transient<IOperationMediator, OperationMediator>();

            Singleton<IHandlerMap, HandlerMap>(isOptional: true);

            Scoped<IHandlerInvoker, HandlerInvoker>();
            Scoped<IHandlerInvokerMiddleware, HandlerInvokerMiddleware>();
            Scoped<IOperationExecutor, OperationExecutor>();
            Scoped<IMiddlewareExecutor, MiddlewareExecutor>();
            Scoped<IOperationContext, OperationContext>();
            Scoped<IOperationStateProvider, OperationStateProvider>();

            Singleton<IOperationNameResolver, OperationNameResolver>();
            Scoped<OperationNameResolutionMiddleware, OperationNameResolutionMiddleware>();
            Scoped<ValidationMiddleware, ValidationMiddleware>();
            Scoped<ExceptionHandlerMiddleware, ExceptionHandlerMiddleware>();

            Component(typeof(IValidationFailedEventHandler), typeof(DefaultValidationFailedEventHandler), ComponentLifetime.Scoped, isOptional: true);
            Component(typeof(IExceptionEventHandler), typeof(DefaultExceptionEventHandler), ComponentLifetime.Scoped, isOptional: true);
            Component(typeof(IUnhandledExceptionEventHandler), typeof(DefaultUnhandledExceptionEventHandler), ComponentLifetime.Scoped, isOptional: true);
            Component(typeof(ILoggingCorrelationProvider), typeof(DefaultLoggingCorrelationProvider), ComponentLifetime.Scoped, isOptional: true);
        }
    }
}
