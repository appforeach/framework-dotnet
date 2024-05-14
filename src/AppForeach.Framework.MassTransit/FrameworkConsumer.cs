using MassTransit;

namespace AppForeach.Framework.MassTransit
{
    public class FrameworkConsumer<TMessage> : IConsumer<TMessage>
        where TMessage : class
    {
        private readonly IOperationMediator operationMediator;

        public FrameworkConsumer(IOperationMediator operationMediator)
        {
            this.operationMediator = operationMediator;
        }

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
