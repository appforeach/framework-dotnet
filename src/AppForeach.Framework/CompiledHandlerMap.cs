using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class CompiledHandlerMap : ICompiledHandlerMap
    {
        private readonly Dictionary<Type, CompiledHandlerInfo> compiledMap;

        public CompiledHandlerMap(IEnumerable<IHandlerDefinition> handlerDefinitions)
        {
            compiledMap = handlerDefinitions.ToDictionary(
                hd => hd.InputType, 
                hd => new CompiledHandlerInfo
                {
                    HandlerFunc = CompileHandler(hd.ImplementationMethod),
                    HandlerType = hd.ImplementationMethod.DeclaringType
                }
            );
        }

        public CompiledHandlerInfo GetHandlerInfo(Type inputType)
        {
            if (compiledMap.ContainsKey(inputType))
            {
                return compiledMap[inputType];
            }
            else
            {
                return null;
            }
        }

        private static Func<object, object, CancellationToken, Task<object>> CompileHandler(MethodInfo method)
        {
            var handlerParam = Expression.Parameter(typeof(object), "handler");
            var requestParam = Expression.Parameter(typeof(object), "request");
            var tokenParam = Expression.Parameter(typeof(CancellationToken), "token");

            var parameters = method.GetParameters();

            var handlerCast = Expression.Convert(handlerParam, method.DeclaringType);

            Expression call;

            if (parameters.Length == 1)
            {
                call = Expression.Call(handlerCast, method, Expression.Convert(requestParam, parameters[0].ParameterType));
            }
            else if (parameters.Length == 2 && parameters[1].ParameterType == typeof(CancellationToken))
            {
                call = Expression.Call(handlerCast, method, Expression.Convert(requestParam, parameters[0].ParameterType), tokenParam);
            }
            else
            {
                string msg = $"Handler method '{method.Name}' in '{method.DeclaringType.FullName}' should have request input parameter and optionally CancellationToken";
                throw new FrameworkException(msg);
            }

            var resultType = method.ReturnType.GenericTypeArguments[0];

            var convertTaskMethod = typeof(CompiledHandlerMap).GetMethod(nameof(ConvertTask), BindingFlags.NonPublic | BindingFlags.Static);
            var helper = convertTaskMethod.MakeGenericMethod(resultType);

            var body = Expression.Call(helper, call);

            var expression = Expression.Lambda<Func<object, object, CancellationToken, Task<object>>>(
                body, handlerParam, requestParam, tokenParam);
            
            return expression.Compile();
        }

        private static async Task<object> ConvertTask<TResult>(Task<TResult> task)
        {
            return await task.ConfigureAwait(false);
        }
    }
}
