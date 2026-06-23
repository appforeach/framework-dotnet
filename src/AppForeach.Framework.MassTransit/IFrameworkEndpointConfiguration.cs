using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public interface IFrameworkEndpointConfiguration
    {
        void AddConsumerInstaller(IConsumerInstaller consumerInstaller);

        IConsumerConfigurationBuilder<TConsumer> Consumer<TMessage, TConsumer>()
            where TMessage : class
            where TConsumer : class, IConsumer<TMessage>;

        IConsumerConfigurationBuilder<FrameworkMediatorConsumer<TMessage>> Mediator<TMessage>(Action<IOperationBuilder>? options=null)
            where TMessage : class;
    }

    public interface IFrameworkEndpointConfiguration<TEndpointConfigurator>
    {
        void Configure(Action<TEndpointConfigurator> endpointAction);

        void Configure(Action<IBusRegistrationContext, TEndpointConfigurator> endpointAction);
    }
}
