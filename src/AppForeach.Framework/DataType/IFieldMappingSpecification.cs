using System;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public interface IFieldMappingSpecification<TFromType>
    {
        void From<TFieldType>(Expression<Func<TFromType, TFieldType>> expression);
    }
}
