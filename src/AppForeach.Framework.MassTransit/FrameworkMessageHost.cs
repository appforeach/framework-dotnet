using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMessageHost
    {
        private string lastEndpoint = string.Empty;

        public MessageHostDefinition HostDefinition { get; } = new MessageHostDefinition();

        protected void ConfigureBus(Action<IBusRegistrationConfigurator> busAction)
        {
            HostDefinition.BusActions.Add(busAction);
        }

        protected void ClearBusConfig()
        {
            HostDefinition.BusActions.Clear();
        }

        protected void ConfigureRabbitMqBus(Action<IRabbitMqBusFactoryConfigurator> rabbitBusAction)
        {
            HostDefinition.RabbitBusActions.Add(rabbitBusAction);
        }

        protected void ClearRabbitMqBusConfig()
        {
            HostDefinition.RabbitBusActions.Clear();
        }

        protected void Endpoint(string endpointName, Action<IFrameworkEndpointConfiguration>? endpointAction = null)
        {
            lastEndpoint = endpointName;

            var endpointConfigurator = new FrameworkEndpointConfiguration(endpointName, HostDefinition);
            endpointAction?.Invoke(endpointConfigurator);
        }

        protected void Consume<TMessage>(Action<IConsumerConfigurator<IConsumer<TMessage>>>? consumerAction = null)
            where TMessage : class
        {
            var consumerInstaller = new FrameworkConsumerInstaller<TMessage>(consumerAction);
            HostDefinition.Consumers.Add((lastEndpoint, consumerInstaller));
        }
    }
}
