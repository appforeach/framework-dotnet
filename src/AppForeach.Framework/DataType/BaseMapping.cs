using System;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseMapping<TTo, TFrom>
        where TTo : new()
    {
        protected IFieldMappingSpecification<TFrom> Field<TFieldType>(Expression<Func<TTo, TFieldType>> expression)
        {
            return new EmptyFieldMappingSpecification<TFrom>();
        }

        protected TTo BaseMap(TFrom from)
        {
            return new TTo();
        }

        private class EmptyFieldMappingSpecification<TEmptyFrom> : IFieldMappingSpecification<TEmptyFrom>
        {
            public void From<TFieldType>(Expression<Func<TEmptyFrom, TFieldType>> expression)
            {

            }
        }

        public virtual TTo MapFrom(TFrom from)
        {
            //TODO: discuss idea to implement mapping here and override it in child classes if needed
            return new TTo();
        }
    }
}
