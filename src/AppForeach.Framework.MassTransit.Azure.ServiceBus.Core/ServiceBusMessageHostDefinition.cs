using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusMessageHostDefinition : MessageHostDefinition
{
    public List<Action<IBusRegistrationContext, IServiceBusBusFactoryConfigurator>> RabbitBusActions { get; } = new();

    public List<(string, Action<IBusRegistrationContext, IServiceBusReceiveEndpointConfigurator>)> EndpointActions { get; } = new();
}
