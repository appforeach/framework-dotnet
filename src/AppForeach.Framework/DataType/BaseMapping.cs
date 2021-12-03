using System;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseMapping<TTo, TFrom>
    {
        protected IFieldMappingSpecification<TFrom> Field<TFieldType>(Expression<Func<TTo, TFieldType>> expression)
        {
            return null;
        }

        protected TTo BaseMap(TFrom from)
        {
            return default(TTo);
        }
    }
}
