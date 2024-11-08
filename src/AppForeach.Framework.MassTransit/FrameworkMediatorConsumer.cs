using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMediatorConsumer<TMessage>
        (
        IOperationMediator operationMediator
        ) : IConsumer<TMessage>
        where TMessage : class
    {

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            var result = await operationMediator.Execute(context.Message);

            if(result.Outcome != OperationOutcome.Success) 
            {
                throw new FrameworkException("Operation is not successfull");
            }
        }
    }
}
