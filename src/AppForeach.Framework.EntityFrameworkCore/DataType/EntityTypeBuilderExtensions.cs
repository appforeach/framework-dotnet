using AppForeach.Framework.DataType;
using AppForeach.Framework.DataType.Facets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppForeach.Framework.EntityFrameworkCore.DataType
{
    public static class EntityTypeBuilderExtensions
    {
        public static void FromEntitySpecification<T>(this EntityTypeBuilder<T> builder, BaseEntitySpecification<T> entitySpecification) where T : class
        {
            //todo: make field name part of specification
            foreach (var fieldSpecification in entitySpecification.FieldSpecifications)
            {
                var facets = fieldSpecification.Value.Configuration;

                var propertyBuilder = builder.Property(fieldSpecification.Key);
          
                var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                if (requiredFacet is not null)
                {
                    propertyBuilder.IsRequired(requiredFacet.Required);
                }

                var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                if (requiredFacet is not null)
                {
                    propertyBuilder.HasMaxLength(maxLengthFacet.MaxLength);
                }
            }
        }
    }
}
