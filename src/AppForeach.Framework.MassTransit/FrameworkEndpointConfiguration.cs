using MassTransit;

namespace AppForeach.Framework.MassTransit;

public class FrameworkEndpointConfiguration
    (
        string endpointName,
        MessageHostDefinition hostDefinition
    ): IFrameworkEndpointConfiguration
{

    public void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction)
    {
        hostDefinition.EndpointActions.Add((endpointName, endpointAction));
    }

    public void AddConsumerInstaller(IConsumerInstaller consumerInstaller)
    {
        hostDefinition.Consumers.Add((endpointName, consumerInstaller));
    }

    public IConsumerConfigurationBuilder<TConsumer> Consumer<TMessage, TConsumer>()
        where TMessage : class
        where TConsumer : class, IConsumer<TMessage>
    {
        var consumerInstaller = new FrameworkConsumerInstaller<TMessage, TConsumer>();

        AddConsumerInstaller(consumerInstaller);

        return consumerInstaller.ConfigurationBuilder;
    }

    public IConsumerConfigurationBuilder<FrameworkMediatorConsumer<TMessage>> Mediator<TMessage>() where TMessage : class
    {
        var consumerInstaller = new FrameworkMediatorConsumerInstaller<TMessage>();

        AddConsumerInstaller(consumerInstaller);

        return consumerInstaller.ConfigurationBuilder;
    }
}
