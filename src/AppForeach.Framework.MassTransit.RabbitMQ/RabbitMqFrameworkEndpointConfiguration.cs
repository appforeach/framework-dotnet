using MassTransit;

namespace AppForeach.Framework.MassTransit;

public class RabbitMqFrameworkEndpointConfiguration 
     : FrameworkEndpointConfiguration, IRabbitMqFrameworkEndpointConfiguration
{
    private readonly RabbitMqMessageHostDefinition hostDefinition;

    public RabbitMqFrameworkEndpointConfiguration(string endpointName,
        RabbitMqMessageHostDefinition hostDefinition) : base(endpointName, hostDefinition)
    {
        this.hostDefinition = hostDefinition;
    }

    public void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((EndpointName, (_, cfg) => endpointAction(cfg)));
    }

    public void Configure(Action<IBusRegistrationContext, IRabbitMqReceiveEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((EndpointName, endpointAction));
    }
}
