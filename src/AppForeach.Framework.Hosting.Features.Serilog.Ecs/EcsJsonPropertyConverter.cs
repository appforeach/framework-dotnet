using AppForeach.Framework.Hosting.Features.Logging;
using System.Text.Json;

namespace AppForeach.Framework.Hosting.Features.Serilog.Ecs
{
    public class EcsJsonPropertyConverter : JsonPropertyConverter
    {
        private readonly HashSet<string> notExpandableProperties;

        public EcsJsonPropertyConverter(HashSet<string> notExpandableProperties)
        {
            this.notExpandableProperties = notExpandableProperties;
        }

        protected override JsonCombinedPropertyWriter CreatePropertyWriter(Utf8JsonWriter jsonWriter)
        {
            return new EcsJsonCombinedPropertyWriter(jsonWriter, notExpandableProperties);
        }
    }
}
