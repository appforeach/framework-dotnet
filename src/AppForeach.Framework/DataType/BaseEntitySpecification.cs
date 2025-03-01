﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseEntitySpecification<TType>: BaseEntitySpecification
    {
        private readonly Dictionary<string, object> _fieldSpecifications = new Dictionary<string, object>();
        public override IReadOnlyDictionary<string, IPrimitiveFieldSpecification> FieldSpecifications => _fieldSpecifications.ToDictionary(x => x.Key, x => (IPrimitiveFieldSpecification)x.Value);
        public IPrimitiveFieldSpecification<TFieldType> Field<TFieldType>(Expression<Func<TType, TFieldType>> selector)
        {
            //hint IPrimitiveFIeldSpecification
            //hint: base nongeneric inteface IPrimitiveTypeSpecification
            //todo: throw exception if selector is not a member expression
            var fieldKey = ((MemberExpression)selector.Body).Member.Name;

            if (!_fieldSpecifications.TryGetValue(fieldKey, out object field))
            {
                field = new PrimitiveFieldSpecification<TFieldType>();
                _fieldSpecifications[fieldKey] = field;
            }

            return field as IPrimitiveFieldSpecification<TFieldType>;
        }
    }

    public abstract class BaseEntitySpecification
    {
        public virtual IReadOnlyDictionary<string, IPrimitiveFieldSpecification> FieldSpecifications { get; }
    }
}
