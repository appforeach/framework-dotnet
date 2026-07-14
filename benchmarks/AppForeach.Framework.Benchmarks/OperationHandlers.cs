using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppForeach.Framework.Benchmarks;

public static class OperationHandlers
{
    public static void AddCompiledOperationHandlers(this IServiceCollection services, IList<Assembly> assemblies)
    {
        var handlerMap = assemblies.SelectMany(CreateHandlerMap);
        var handlers = handlerMap.Select(kvp => new HandlerDefinition
        {
            InputType = kvp.Key,
            ImplementationMethod = kvp.Value,
        });


        services.AddSingleton<ICompiledHandlerMap>(new CompiledHandlerMap(handlers));

        foreach (var pair in handlerMap)
        {
            services.AddScoped(pair.Value.DeclaringType!);
        }
    }

    public static void AddOperationHandlers(this IServiceCollection services, IList<Assembly> assemblies)
    {
        var handlerMap = assemblies.SelectMany(CreateHandlerMap);
        var handlers = handlerMap.Select(kvp => new HandlerDefinition
        {
            InputType = kvp.Key,
            ImplementationMethod = kvp.Value,
        });


        services.AddSingleton<IHandlerMap>(new HandlerMap(handlers));

        foreach (var pair in handlerMap)
        {
            services.AddScoped(pair.Value.DeclaringType!);
        }
    }

    private static Dictionary<Type, MethodInfo> CreateHandlerMap(Assembly assembly)
    {
        Dictionary<Type, MethodInfo> map = new Dictionary<Type, MethodInfo>();

        foreach (var type in assembly.GetTypes().Where(t => t.Name.EndsWith("Handler")))
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                var firstParameter = method.GetParameters().FirstOrDefault();

                if (firstParameter != null && (firstParameter.ParameterType.Name.EndsWith("Command")
                    || firstParameter.ParameterType.Name.EndsWith("Query")))
                {
                    map.Add(firstParameter.ParameterType, method);
                }
            }
        }

        return map;
    }
}
