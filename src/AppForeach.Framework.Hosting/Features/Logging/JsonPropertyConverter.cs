using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    public class JsonPropertyConverter
    {
        public string GetJson(IEnumerable<(string PropertyName, object Value)> properties)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions() { Indented = false});

            var propertyWriter = CreatePropertyWriter(writer);

            propertyWriter.WriteStart();

            foreach (var property in properties)
            {
                WriteProperty(property.PropertyName, property.Value, propertyWriter);
            }

            propertyWriter.WriteEnd();

            writer.Flush();
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        protected virtual JsonCombinedPropertyWriter CreatePropertyWriter(Utf8JsonWriter jsonWriter)
            => new JsonCombinedPropertyWriter(jsonWriter);

        private void WriteProperty(string propertyName, object value, JsonCombinedPropertyWriter writer)
        {
            switch (value)
            {
                case string stringValue:
                    writer.WriteProperty(propertyName, stringValue);
                    break;
                case int intValue:
                    writer.WriteProperty(propertyName, intValue);
                    break;
                case long longValue:
                    writer.WriteProperty(propertyName, longValue);
                    break;
                case float floatValue:
                    writer.WriteProperty(propertyName, floatValue);
                    break;
                case double doubleValue:
                    writer.WriteProperty(propertyName, doubleValue);
                    break;
                case decimal decimalValue:
                    writer.WriteProperty(propertyName, decimalValue);
                    break;
                case bool boolValue:
                    writer.WriteProperty(propertyName, boolValue);
                    break;
                case DateTimeOffset dateValue:
                    writer.WriteProperty(propertyName, dateValue);
                    break;
                case string[] arrayValue:
                    writer.WriteProperty(propertyName, arrayValue);
                    break;
            }
        }
    }
}
