using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseEntitySpecification<TType>
    {
        private readonly Dictionary<string, object> _fields = new Dictionary<string, object>();
        public IPrimitiveTypeSpecification<TFieldType> Field<TFieldType>(Expression<Func<TType, TFieldType>> selector)
        {
            //todo: throw exception if selector is not a member expression
            var fieldKey = ((MemberExpression)selector.Body).Member.Name;

            if (!_fields.TryGetValue(fieldKey, out object field))
            {
                field = new PrimitiveTypeSpecification<TFieldType>();
                _fields[fieldKey] = field;
            }

            return field as IPrimitiveTypeSpecification<TFieldType>;
        }
    }
}
