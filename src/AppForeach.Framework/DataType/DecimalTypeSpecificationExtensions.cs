
namespace AppForeach.Framework.DataType
{
    public static class DecimalTypeSpecificationExtensions
    {
        public static IPrimitiveTypeSpecification<decimal> MinValue(this IPrimitiveTypeSpecification<decimal> spec, decimal minValue) => null;

        public static IPrimitiveTypeSpecification<decimal> MaxValue(this IPrimitiveTypeSpecification<decimal> spec, decimal maxValue) => null;

        public static IPrimitiveTypeSpecification<decimal> Digits(this IPrimitiveTypeSpecification<decimal> spec, int total, int precision) => null;
    }
}
