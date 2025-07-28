using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AppForeach.Framework.DataType
{
    public class BaseEntitySpecification<TType>: BaseEntitySpecification
    {
        private readonly Dictionary<string, FacetBag> _fieldSpecifications = new Dictionary<string, FacetBag>();
        private readonly Dictionary<Type, FacetBag> _typeSpecifications = new Dictionary<Type, FacetBag>();
        
        public override IReadOnlyDictionary<string, IPrimitiveFieldSpecification> FieldSpecifications
        {
            get
            {
                Dictionary<string, IPrimitiveFieldSpecification> specifications = new Dictionary<string, IPrimitiveFieldSpecification>();

                foreach(var property in typeof(TType).GetProperties())
                {
                    FacetBag facets = null;
                    var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    _typeSpecifications.TryGetValue(propertyType, out facets);

                    if(_fieldSpecifications.TryGetValue(property.Name, out var fieldFacets))
                    {
                        if(facets == null)
                        {
                            facets = fieldFacets;
                        }
                        else
                        {
                            facets = facets.Combine(fieldFacets);
                        }
                    }

                    if (facets != null)
                    {
                        specifications[property.Name] = new PrimitiveFieldSpecification(facets);
                    }
                }

                return specifications;
            }
        }            
        
        public IPrimitiveFieldSpecification<TFieldType> Field<TFieldType>(Expression<Func<TType, TFieldType>> selector)
        {
            //hint IPrimitiveFIeldSpecification
            //hint: base nongeneric inteface IPrimitiveTypeSpecification
            //todo: throw exception if selector is not a member expression
            var fieldKey = ((MemberExpression)selector.Body).Member.Name;

            if (!_fieldSpecifications.TryGetValue(fieldKey, out FacetBag facets))
            {
                facets = new FacetBag();
                _fieldSpecifications[fieldKey] = facets;
            }

            return new PrimitiveFieldSpecification<TFieldType>(facets);
        }

        public IPrimitiveFieldSpecification<TFieldType> Type<TFieldType>()
        {
            if(!_typeSpecifications.TryGetValue(typeof(TFieldType), out FacetBag facets))
            {
                facets = new FacetBag();
                _typeSpecifications[typeof(TFieldType)] = facets;
            }

            return new PrimitiveFieldSpecification<TFieldType>(facets);
        }
    }

    public abstract class BaseEntitySpecification
    {
        public abstract IReadOnlyDictionary<string, IPrimitiveFieldSpecification> FieldSpecifications { get; }
    }
}
