using MassTransit;
using MassTransit.Configuration;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMessageHostInstaller
    {
        private readonly MessageHostDefinition hostDefinition;

        public FrameworkMessageHostInstaller(MessageHostDefinition hostDefinition)
        {
            this.hostDefinition = hostDefinition;
        }

        public void Setup(IBusRegistrationConfigurator bus)
        {
            ArgumentNullException.ThrowIfNull(bus);

            foreach (var busAction in hostDefinition.BusActions)
            {
                busAction(bus);
            }

            foreach (var consumer in hostDefinition.Consumers)
            {
                consumer.Item2.AddConsumer(bus);
            }

            //TODO: while first implementation is tied to RabbitMq transport it should be refactored later
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
