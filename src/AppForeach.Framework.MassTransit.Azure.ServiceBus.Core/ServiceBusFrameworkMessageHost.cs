using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusFrameworkMessageHost : FrameworkMessageHost<IServiceBusBusFactoryConfigurator, IServiceBusReceiveEndpointConfigurator>
{
}
