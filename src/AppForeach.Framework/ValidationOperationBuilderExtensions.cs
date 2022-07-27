
namespace AppForeach.Framework
{
    public static class ValidationOperationBuilderExtensions
    {
        public static IOperationBuilder HasValidator(this IOperationBuilder builder, bool hasValidator)
        {
            var facet = new ValidationHasValidatorFacet
            {
                HasValidator = hasValidator
            };

            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
