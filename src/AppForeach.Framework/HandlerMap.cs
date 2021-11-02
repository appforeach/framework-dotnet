using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppForeach.Framework
{
    public class HandlerMap : IHandlerMap
    {
        private readonly Dictionary<Type, MethodInfo> map;

        public HandlerMap(Dictionary<Type, MethodInfo> map)
        {
            this.map = map;
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
