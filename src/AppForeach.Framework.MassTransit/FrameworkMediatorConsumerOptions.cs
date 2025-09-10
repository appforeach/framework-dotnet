namespace AppForeach.Framework.MassTransit
{
    public class FrameworkMediatorConsumerOptions<TMessage>
    {
        public Action<IOperationBuilder>? OperationOptions { get; set; }
    }
}
