using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class ValidatorMap : IValidatorMap
    {
        private readonly Dictionary<Type, Type> map;

        public ValidatorMap(Dictionary<Type, Type> map)
        {
            this.map = map;
        }

        public Type GetValidatorType(Type inputType)
        {
            if(map.ContainsKey(inputType))
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
