using System;
using System.Collections.Generic;
using System.Linq;

namespace AppForeach.Framework.Validation
{
    public class ValidatorMap : IValidatorMap
    {
        private readonly Dictionary<Type, Type> map;

        public ValidatorMap(IEnumerable<IValidatorDefinition> validatorDefinitions)
        {
            this.map = validatorDefinitions.ToDictionary(vd => vd.InputType, vd => vd.ValidatorType);
        }

        public Type GetValidatorType(Type inputType)
        {
            if (map.ContainsKey(inputType))
            {
                return map[inputType];
            }
            else
            {
                return null;
            }
        }
    }
}
