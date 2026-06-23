using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusFrameworkEndpointConfiguration
     : FrameworkEndpointConfiguration, IServiceBusFrameworkEndpointConfiguration
{
    private readonly ServiceBusMessageHostDefinition hostDefinition;

    public ServiceBusFrameworkEndpointConfiguration(string endpointName,
        ServiceBusMessageHostDefinition hostDefinition) : base(endpointName, hostDefinition)
    {
        this.hostDefinition = hostDefinition;
    }

    public void Configure(Action<IServiceBusReceiveEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((EndpointName, (_, cfg) => endpointAction(cfg)));
    }

    public void Configure(Action<IBusRegistrationContext, IServiceBusReceiveEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((EndpointName, endpointAction));
    }
}

