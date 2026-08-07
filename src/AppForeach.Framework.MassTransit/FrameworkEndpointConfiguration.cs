using MassTransit;

namespace AppForeach.Framework.MassTransit;

public class FrameworkEndpointConfiguration<TBusFactoryConfigurator, TEndpointConfigurator> 
    : IFrameworkEndpointConfiguration<TEndpointConfigurator>
{
    private readonly string endpointName;
    private readonly MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator> hostDefinition;

    public FrameworkEndpointConfiguration(string endpointName,
        MessageHostDefinition<TBusFactoryConfigurator, TEndpointConfigurator> hostDefinition)
    {
        this.endpointName = endpointName;
        this.hostDefinition = hostDefinition;
    }

    public void AddConsumerInstaller(IConsumerInstaller consumerInstaller)
    {
        ArgumentNullException.ThrowIfNull(consumerInstaller);

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

    public IConsumerConfigurationBuilder<FrameworkMediatorConsumer<TMessage>> Mediator<TMessage>(Action<IOperationBuilder>? options = null) where TMessage : class
    {
        var consumerInstaller = new FrameworkMediatorConsumerInstaller<TMessage>(options);

        AddConsumerInstaller(consumerInstaller);

        return consumerInstaller.ConfigurationBuilder;
    }

    public void Configure(Action<TEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((endpointName, (_, cfg) => endpointAction(cfg)));
    }

    public void Configure(Action<IBusRegistrationContext, TEndpointConfigurator> endpointAction)
    {
        ArgumentNullException.ThrowIfNull(endpointAction);

        hostDefinition.EndpointActions.Add((endpointName, endpointAction));
    }
}
