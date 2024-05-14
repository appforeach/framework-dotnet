using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkConsumerInstaller<TMessage> : IConsumerInstaller
        where TMessage : class
    {
        private readonly Action<IConsumerConfigurator<IConsumer<TMessage>>>? consumerAction;

        public FrameworkConsumerInstaller(Action<IConsumerConfigurator<IConsumer<TMessage>>>? consumerAction = null)
        {
            this.consumerAction = consumerAction;
        }

        public void AddConsumer(IRegistrationConfigurator registrationConfigurator)
        {
            registrationConfigurator.AddConsumer<FrameworkConsumer<TMessage>>();
        }

        public void ConfigureConsumer(IReceiveEndpointConfigurator receiveEndpointConfigurator, IRegistrationContext registration)
        {
            Action<IConsumerConfigurator<IConsumer<TMessage>>> action = consumerAction!;

            //receiveEndpointConfigurator.ConfigureConsumer<IConsumer<TMessage>>(registration, action);

            receiveEndpointConfigurator.ConfigureConsumer<FrameworkConsumer<TMessage>>(registration);

            //receiveEndpointConfigurator.ConfigureConsumer<FrameworkConsumer<TMessage>>(registration, cfg =>
            //{
            //    cfg.UseConcurrentMessageLimit
            //    consumerAction(cfg);
            //});
        }
    }
}
