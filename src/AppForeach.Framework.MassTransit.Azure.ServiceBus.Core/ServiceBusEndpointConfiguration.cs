using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusEndpointConfiguration : EndpointConfiguration
{

    public Action<IServiceBusReceiveEndpointConfigurator>? EndpointAction { get; set; }
}
