
namespace AppForeach.Framework
{
    public static class OperationNameOperationBuilderExtensions
    {
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
