using System;
using System.Collections.Generic;
using System.Text;
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
            
            if(output.Outcome != OperationOutcome.Success)
            {
                throw new FrameworkException("Operation is not successfull");
            }

            return (TOutput)output.Result;
        }

        public static IOperationBuilder OperationName(this IOperationBuilder builder, string operationName)
        {
            var spec = builder.Configuration.Get<OperationSpecificationConfiguration>();

            spec.OperationName = operationName;

            return builder;
        }

        public static IOperationBuilder IsCommand(this IOperationBuilder builder, bool isCommand)
        {
            var spec = builder.Configuration.Get<OperationSpecificationConfiguration>();

            spec.IsCommand = isCommand;

            return builder;
        }
    }
}
