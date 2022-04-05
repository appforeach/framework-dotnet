using AppForeach.Framework;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EscapeHit.Invoice.WebApi
{
    public class CustomSwaggerOperationFilter : IOperationFilter
    {
        private readonly IOperationNameResolver operationNameResolver;
        private readonly IHandlerMap handlerMap;

        public CustomSwaggerOperationFilter(IOperationNameResolver operationNameResolver, IHandlerMap handlerMap)
        {
            this.operationNameResolver = operationNameResolver;
            this.handlerMap = handlerMap;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodParameters = context.MethodInfo.GetParameters();

            if(methodParameters.Length == 1)
            {
                var paramType = methodParameters[0].ParameterType;
                var handlerType = handlerMap.GetHandlerMethod(paramType);

                if (handlerType != null)
                {
                    var resolvedName = operationNameResolver.ResolveName(paramType, handlerType.DeclaringType);
                    operation.OperationId = resolvedName.Name;
                }
            }
        }
    }
}
