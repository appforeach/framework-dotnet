using MassTransit;

namespace AppForeach.Framework.MassTransit.Azure.ServiceBus.Core;

public class ServiceBusFrameworkMessageHost : FrameworkMessageHost<ServiceBusMessageHostDefinition>
{
    private string lastEndpoint = string.Empty;

    protected void ConfigureRabbitMqBus(Action<IServiceBusBusFactoryConfigurator> rabbitBusAction)
    {
        ConfigureRabbitMqBus((context, config) => rabbitBusAction(config));
    }

    protected void ConfigureRabbitMqBus(Action<IBusRegistrationContext, IServiceBusBusFactoryConfigurator> rabbitBusAction)
    {
        HostDefinition.RabbitBusActions.Add(rabbitBusAction);
    }

    protected void ClearRabbitMqBusConfig()
    {
        HostDefinition.RabbitBusActions.Clear();
    }

    protected void Endpoint(string endpointName, Action<IServiceBusFrameworkEndpointConfiguration>? endpointAction = null)
    {
        lastEndpoint = endpointName;

        var endpointConfigurator = new ServiceBusFrameworkEndpointConfiguration(endpointName, HostDefinition);
        endpointAction?.Invoke(endpointConfigurator);
    }
}
