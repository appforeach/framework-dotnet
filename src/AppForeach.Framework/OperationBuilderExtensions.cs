using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public static class OperationBuilderExtensions
    {
        public static async Task<TOutput> As<TOutput>(this IOperationBuilder builder)
        {
            var input = builder.Configuration.Get<OperationExecutionInputConfiguration>();

            var executor = input.Executor;

            var output = await executor.Execute(builder.Configuration);

            if (output.Outcome != OperationOutcome.Success)
            {
                throw new FrameworkException("Operation is not successfull");
            }

            return (TOutput)output.Result;
        }
    }
}
