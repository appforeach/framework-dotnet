using System;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseEntitySpecification<TType>
    {
        public IPrimitiveTypeSpecification<TFieldType> Field<TFieldType>(Expression<Func<TType, TFieldType>> expression)
        {
            return null;
        }
    }
}
