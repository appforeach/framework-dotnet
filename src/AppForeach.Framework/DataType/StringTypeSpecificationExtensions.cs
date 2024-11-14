
using AppForeach.Framework.DataType.Facets;

namespace AppForeach.Framework.DataType
{
    public static class StringTypeSpecificationExtensions
    {
        public static IPrimitiveTypeSpecification<string> MaxLength(this IPrimitiveTypeSpecification<string> spec, int maxLength)
        {
            spec.Configuration.Set(new FieldMaxLengthFacet { MaxLength = maxLength });
            return spec;
        }

        public static IPrimitiveTypeSpecification<string> Pattern(this IPrimitiveTypeSpecification<string> spec, string pattern) => null;
    }
}
