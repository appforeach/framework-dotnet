using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkEndpointConfiguration : IFrameworkEndpointConfiguration
    {
        private readonly string endpointName;
        private readonly MessageHostDefinition hostDefinition;

        public FrameworkEndpointConfiguration(
            string endpointName,
            MessageHostDefinition hostDefinition)
        {
            this.endpointName = endpointName;
            this.hostDefinition = hostDefinition;
        }

        public void Configure(Action<IRabbitMqReceiveEndpointConfigurator> endpointAction)
        {
            hostDefinition.EndpointActions.Add((endpointName, endpointAction));
        }

        public void Consume<TMessage>(Action<IConsumerConfigurator<IConsumer<TMessage>>>? consumerAction = null) 
            where TMessage : class
        {
            var consumerInstaller = new FrameworkConsumerInstaller<TMessage>(consumerAction);
            hostDefinition.Consumers.Add((endpointName, consumerInstaller));
        }
    }
}
