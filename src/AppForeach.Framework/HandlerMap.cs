using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppForeach.Framework
{
    public class HandlerMap : IHandlerMap
    {
        private readonly Dictionary<Type, MethodInfo> map;

        public HandlerMap(IEnumerable<IHandlerDefinition> handlerDefinitions)
        {
            this.map = handlerDefinitions.ToDictionary(hd => hd.InputType, hd => hd.ImplementationMethod);
        }

        public MethodInfo GetHandlerMethod(Type inputType)
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
