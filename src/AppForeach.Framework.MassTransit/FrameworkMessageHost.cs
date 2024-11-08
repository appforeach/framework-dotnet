using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMessageHost
    {
        private string lastEndpoint = string.Empty;

        public MessageHostDefinition HostDefinition { get; } = new MessageHostDefinition();

        public bool IsMediatorEnabled { get; set; } = true;

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
            ConfigureRabbitMqBus((context, config) => rabbitBusAction(config));
        }

        protected void ConfigureRabbitMqBus(Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> rabbitBusAction)
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
    }
}
