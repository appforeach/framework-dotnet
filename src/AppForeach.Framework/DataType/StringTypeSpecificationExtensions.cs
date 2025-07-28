
using AppForeach.Framework.DataType.Facets;

namespace AppForeach.Framework.DataType
{
    public static class StringTypeSpecificationExtensions
    {
        public static IPrimitiveFieldSpecification<string> HasMaxLength(this IPrimitiveFieldSpecification<string> spec, int maxLength)
        {
            spec.Configuration.Set(new FieldMaxLengthFacet { MaxLength = maxLength });
            return spec;
        }

        public static IPrimitiveFieldSpecification<string> IsEmptyAllowed(this IPrimitiveFieldSpecification<string> spec, bool isEmptyAllowed)
        {
            spec.Configuration.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = isEmptyAllowed });
            return spec;
        }
    }
}
