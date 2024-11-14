
using AppForeach.Framework.DataType.Facets;

namespace AppForeach.Framework.DataType
{
    public static class StringTypeSpecificationExtensions
    {
        public static IPrimitiveFieldSpecification<string> MaxLength(this IPrimitiveFieldSpecification<string> spec, int maxLength)
        {
            spec.Configuration.Set(new FieldMaxLengthFacet { MaxLength = maxLength });
            return spec;
        }

        public static IPrimitiveFieldSpecification<string> Pattern(this IPrimitiveFieldSpecification<string> spec, string pattern) => null;
    }
}
