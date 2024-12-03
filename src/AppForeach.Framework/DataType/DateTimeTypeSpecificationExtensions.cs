using System;

namespace AppForeach.Framework.DataType
{
    public static class DateTimeTypeSpecificationExtensions
    {
        public static IPrimitiveFieldSpecification<DateTime> MinValue(this IPrimitiveFieldSpecification<DateTime> spec, DateTime minValue) => null;

        public static IPrimitiveFieldSpecification<DateTime> MinValue(this IPrimitiveFieldSpecification<DateTime> spec, int year, int month, int day) => null;

        public static IPrimitiveFieldSpecification<DateTime> MaxValue(this IPrimitiveFieldSpecification<DateTime> spec, DateTime maxValue) => null;

        public static IPrimitiveFieldSpecification<DateTime> MaxValue(this IPrimitiveFieldSpecification<DateTime> spec, int year, int month, int day) => null;
    }
}
