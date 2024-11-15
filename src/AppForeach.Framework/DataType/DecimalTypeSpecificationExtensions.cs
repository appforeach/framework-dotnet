
namespace AppForeach.Framework.DataType
{
    public static class DecimalTypeSpecificationExtensions
    {
        public static IPrimitiveFieldSpecification<decimal> MinValue(this IPrimitiveFieldSpecification<decimal> spec, decimal minValue) => null;

        public static IPrimitiveFieldSpecification<decimal> MaxValue(this IPrimitiveFieldSpecification<decimal> spec, decimal maxValue) => null;

        public static IPrimitiveFieldSpecification<decimal> Digits(this IPrimitiveFieldSpecification<decimal> spec, int total, int precision) => null;
    }
}
