using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EscapeHit.Invoice.WebApi
{
    public class CustomSwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if(context.Type == typeof(Commands.CreateInvoice.CreateInvoiceCommand))
            {
                var customerNumberProperty = schema.Properties["customerNumber"];
                customerNumberProperty.Nullable = false;
                customerNumberProperty.MaxLength = 10;

                var amountProperty = schema.Properties["amount"];
                amountProperty.Nullable = true;
            }
        }
    }
}
