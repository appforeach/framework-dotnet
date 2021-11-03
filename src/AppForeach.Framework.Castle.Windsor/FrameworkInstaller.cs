using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AppForeach.Framework.Castle.Windsor
{
    public class FrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IServiceLocator>().ImplementedBy<ServiceLocator>());
            
            container.Register(Component.For<IHandlerExecutor>().ImplementedBy<HandlerExecutor>());

            container.Register(Component.For<IOperationMediator>().ImplementedBy<OperationMediator>());

            container.Register(Component.For<IOperationExecutor>().ImplementedBy<OperationExecutor>());

            container.Register(Component.For<IHandlerExecutorMiddleware>().ImplementedBy<HandlerExecutorMiddleware>());
        }
    }
}
