using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public interface IFrameworkEndpointConfiguration
    {
        void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction);

        void Consume<TMessage>(Action<IConsumerConfigurator<IConsumer<TMessage>>>? consumerAction = null)
            where TMessage: class;
    }
}
