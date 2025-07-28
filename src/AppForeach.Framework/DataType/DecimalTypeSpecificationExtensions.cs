
using AppForeach.Framework.DataType.Facets;

namespace AppForeach.Framework.DataType
{
    public static class DecimalTypeSpecificationExtensions
    {
        public static IPrimitiveFieldSpecification<decimal> HasPrecision(this IPrimitiveFieldSpecification<decimal> spec, int precision, int scale)
        {
            spec.Configuration.Set(new FieldPrecisionFacet 
            { 
                Precision = precision,
                Scale = scale
            });

            return spec;
        }

        public static IPrimitiveFieldSpecification<decimal?> HasPrecision(this IPrimitiveFieldSpecification<decimal?> spec, int precision, int scale)
        {
            spec.Configuration.Set(new FieldPrecisionFacet
            {
                Precision = precision,
                Scale = scale
            });

            return spec;
        }
    }
}
