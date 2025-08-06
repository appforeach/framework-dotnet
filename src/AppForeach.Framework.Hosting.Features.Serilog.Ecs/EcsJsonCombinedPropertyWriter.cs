using AppForeach.Framework.Hosting.Features.Logging;
using System.Text.Json;

namespace AppForeach.Framework.Hosting.Features.Serilog.Ecs
{
    public class EcsJsonCombinedPropertyWriter : JsonCombinedPropertyWriter
    {
        private readonly HashSet<string> notExpandableProperties;

        public EcsJsonCombinedPropertyWriter(Utf8JsonWriter jsonWriter, HashSet<string> notExpandableProperties) : base(jsonWriter)
        {
            this.notExpandableProperties = notExpandableProperties;
        }

        protected override bool ShouldExpandProperty(string property)
            => !notExpandableProperties.Contains(property);
    }
}
