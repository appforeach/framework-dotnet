
namespace AppForeach.Framework
{
    public static class ValidationOperationBuilderExtensions
    {
        public static IOperationBuilder HasValidator(this IOperationBuilder builder, bool hasValidator)
        {
            var spec = builder.Configuration.Get<ValidationSpecificationConfiguration>();

            spec.HasValidator = hasValidator;

            return builder;
        }
    }
}
