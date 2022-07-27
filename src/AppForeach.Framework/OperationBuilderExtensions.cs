using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public static class OperationBuilderExtensions
    {
        public static async Task<TOutput> As<TOutput>(this Task<OperationResult> executionTask)
        {
            var output = await executionTask;

            if (output.Outcome != OperationOutcome.Success)
            {
                throw new FrameworkException("Operation is not successfull");
            }

            return (TOutput)output.Result;
        }
    }
}
