using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class RabbitMqFrameworkMessageHost : FrameworkMessageHost<RabbitMqMessageHostDefinition>
    {
        private string lastEndpoint = string.Empty;

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

        protected void Endpoint(string endpointName, Action<IRabbitMqFrameworkEndpointConfiguration>? endpointAction = null)
        {
            lastEndpoint = endpointName;

            var endpointConfigurator = new RabbitMqFrameworkEndpointConfiguration(endpointName, HostDefinition);
            endpointAction?.Invoke(endpointConfigurator);
        }
    }
}
