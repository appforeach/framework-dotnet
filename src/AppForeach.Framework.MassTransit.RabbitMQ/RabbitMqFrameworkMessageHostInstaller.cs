using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class RabbitMqFrameworkMessageHostInstaller : FrameworkMessageHostInstaller<RabbitMqMessageHostDefinition>
    {
        public RabbitMqFrameworkMessageHostInstaller(RabbitMqMessageHostDefinition hostDefinition)
            : base(hostDefinition)
        {
        }

        public override void Setup(IBusRegistrationConfigurator bus)
        {
            ArgumentNullException.ThrowIfNull(bus);

            base.Setup(bus);

            bus.UsingRabbitMq((context, cfg) =>
            {
                foreach(var rabbitMqAction in hostDefinition.RabbitBusActions)
                {
                    rabbitMqAction.Invoke(context, cfg);
                }

                SetupEndpoints(context, cfg);

                cfg.ConfigureEndpoints(context);
            });
        }

        private void SetupEndpoints(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator busConfigurator)
        {
            foreach(var endpoint in hostDefinition.Consumers.GroupBy(c => c.Item1))
            {
                string endpointName = endpoint.Key;

                busConfigurator.ReceiveEndpoint(endpointName, endpointConfig =>
                {
                    foreach(var endpointAction in hostDefinition.EndpointActions.Where(ea => ea.Item1 == endpointName))
                    {
                        endpointAction.Item2(context, endpointConfig);
                    }

                    foreach(var consumerInstaller in endpoint.Select(e => e.Item2))
                    {
                        consumerInstaller.ConfigureConsumer(endpointConfig, context);
                    }
                });
            }
        }
    }
}
