using System.Threading.Tasks;
using AppForeach.Framework;
using Microsoft.AspNetCore.Mvc;

namespace EscapeHit.WebApi
{
    public static class OperationResultExtensions
    {
        public static async Task<ActionResult> Ok(this Task<OperationResult> operationResultTask)
        {
            var output = await operationResultTask;

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

        public static async Task<ActionResult> OkOrNotFound(this Task<OperationResult> operationResultTask)
        {
            var output = await operationResultTask;

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
