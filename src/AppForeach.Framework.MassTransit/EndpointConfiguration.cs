

namespace AppForeach.Framework.MassTransit
{
    public class EndpointConfiguration<TEndpointConfigurator> 
    {
        public required string EndpointName { get; set; }

        public Action<TEndpointConfigurator>? EndpointAction { get; set; }
    }
}
