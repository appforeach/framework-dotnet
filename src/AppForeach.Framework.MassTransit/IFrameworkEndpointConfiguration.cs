using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public interface IFrameworkEndpointConfiguration
    {
        void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction);

        void AddConsumerInstaller(IConsumerInstaller consumerInstaller);

        IConsumerConfigurationBuilder<TConsumer> Consumer<TMessage, TConsumer>()
            where TMessage : class
            where TConsumer : class, IConsumer<TMessage>;

        IConsumerConfigurationBuilder<FrameworkMediatorConsumer<TMessage>> Mediator<TMessage>()
            where TMessage : class;
    }
}
