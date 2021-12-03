using System;

namespace AppForeach.Framework.DataType
{
    public static class DateTimeTypeSpecificationExtensions
    {
        public static IPrimitiveTypeSpecification<DateTime> MinValue(this IPrimitiveTypeSpecification<DateTime> spec, DateTime minValue) => null;

        public static IPrimitiveTypeSpecification<DateTime> MinValue(this IPrimitiveTypeSpecification<DateTime> spec, int year, int month, int day) => null;

        public static IPrimitiveTypeSpecification<DateTime> MaxValue(this IPrimitiveTypeSpecification<DateTime> spec, DateTime maxValue) => null;

        public static IPrimitiveTypeSpecification<DateTime> MaxValue(this IPrimitiveTypeSpecification<DateTime> spec, int year, int month, int day) => null;
    }
}
