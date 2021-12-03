using System.Threading.Tasks;
using AppForeach.Framework;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.WebApi
{
    public static class OperationBuilderWebExtensions
    {
        public static async Task<ActionResult> Ok(this IOperationBuilder builder)
        {
            var input = builder.Configuration.Get<OperationExecutionInputConfiguration>();

            var executor = input.Executor;

            var output = await executor.Execute(builder.Configuration);

            if (output.Outcome != OperationOutcome.Success)
            {
                throw new FrameworkException("Operation is not successfull");
            }

            var result = output.Result;

            if(result == null)
            {
                throw new FrameworkException("Operation returned null");
            }

            return new ObjectResult(output.Result);
        }

        public static async Task<ActionResult> OkOrNotFound(this IOperationBuilder builder)
        {
            var input = builder.Configuration.Get<OperationExecutionInputConfiguration>();

            var executor = input.Executor;

            var output = await executor.Execute(builder.Configuration);

            if (output.Outcome != OperationOutcome.Success)
            {
                throw new FrameworkException("Operation is not successfull");
            }

            var result = output.Result;

            if (result == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new ObjectResult(output.Result);
            }            
        }
    }
}
