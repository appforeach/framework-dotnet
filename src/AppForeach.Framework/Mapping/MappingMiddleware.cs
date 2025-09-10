using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Mapping
{
    public class MappingMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext operationContext;
        private readonly IFrameworkMapper mapper;

        public MappingMiddleware(IOperationContext operationContext, IFrameworkMapper mapper)
        {
            this.operationContext = operationContext;
            this.mapper = mapper;
        }

        public async Task ExecuteAsync(NextOperationDelegate next, CancellationToken cancellationToken)
        {
            var mappingFacet = operationContext.Configuration.TryGet<MappingDestinationTypeFacet>();

            if(mappingFacet?.DestinationType != null)
            {
                var contextState = operationContext.State.Get<OperationContextState>();

                if (contextState.Input == null)
                {
                    throw new FrameworkException("Operation input is null.");
                }

                contextState.Input = mapper.Map(contextState.Input, mappingFacet.DestinationType);
            }

            await next();
        }
    }
}
