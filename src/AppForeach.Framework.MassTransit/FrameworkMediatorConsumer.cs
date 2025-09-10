using MassTransit;
using Microsoft.Extensions.Options;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMediatorConsumer<TMessage>
        (
        IOperationMediator operationMediator,
        IOptionsSnapshot<FrameworkMediatorConsumerOptions<TMessage>> options
        ) : IConsumer<TMessage>
        where TMessage : class
    {

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            var result = await operationMediator.Execute(context.Message, options.Value?.OperationOptions);

            if(result.Outcome != OperationOutcome.Success) 
            {
                throw new FrameworkException("Operation is not successfull");
            }
        }
    }
}
