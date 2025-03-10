using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    public class JsonCombinedPropertyWriter
    {
        private readonly Utf8JsonWriter jsonWriter;
        private readonly List<string> currentIndent = new List<string>();

        public JsonCombinedPropertyWriter(Utf8JsonWriter jsonWriter)
        {
            this.jsonWriter = jsonWriter;
        }

        public void WriteStart()
        {
            jsonWriter.WriteStartObject();
        }

        public void WriteProperty(string property, int value)
        {
            WritePropertyName(property);
            jsonWriter.WriteNumberValue(value);
        }

        public void WriteProperty(string property, string value)
        {
            WritePropertyName(property);
            jsonWriter.WriteStringValue(value);
        }

        public void WriteProperty(string property, long value)
        {
            WritePropertyName(property);
            jsonWriter.WriteNumberValue(value);
        }

        public void WriteProperty(string property, decimal value)
        {
            WritePropertyName(property);
            jsonWriter.WriteNumberValue(value);
        }

        public void WriteProperty(string property, float value)
        {
            WritePropertyName(property);
            jsonWriter.WriteNumberValue(value);
        }

        public void WriteProperty(string property, bool value)
        {
            WritePropertyName(property);
            jsonWriter.WriteBooleanValue(value);
        }

        public void WriteProperty(string property, DateTimeOffset value)
        {
            WritePropertyName(property);
            jsonWriter.WriteStringValue(value);
        }

        public void WriteProperty(string property, string[] value)
        {
            WritePropertyName(property);

            jsonWriter.WriteStartArray();

            foreach (string valueItem in value)
            {
                jsonWriter.WriteStringValue(valueItem);
            }

            jsonWriter.WriteEndArray();
        }

        protected virtual bool ShouldExpandProperty(string property) => true;

        private void WritePropertyName(string property)
        {
            bool shouldExpandProperty = ShouldExpandProperty(property);
            int commonIndent = 0;
            string[] propertyPath = [];
            string propertyName = property;

            if (shouldExpandProperty)
            {
                string pathAndPropertyName = property;
                int propertyNameStartIndex = pathAndPropertyName.LastIndexOf('.');
                propertyName = pathAndPropertyName.Substring(propertyNameStartIndex + 1);

                if (propertyNameStartIndex > 0)
                {
                    propertyPath = pathAndPropertyName.Substring(0, propertyNameStartIndex).Split('.');
                }

                while (commonIndent < currentIndent.Count && commonIndent < propertyPath.Length
                    && string.Equals(currentIndent[commonIndent], propertyPath[commonIndent], StringComparison.Ordinal))
                {
                    commonIndent++;
                }
            }

            while (currentIndent.Count > commonIndent)
            {
                jsonWriter.WriteEndObject();
                currentIndent.RemoveAt(currentIndent.Count - 1);
            }

            while (currentIndent.Count < propertyPath.Length)
            {
                jsonWriter.WritePropertyName(propertyPath[currentIndent.Count]);
                jsonWriter.WriteStartObject();
                currentIndent.Add(propertyPath[currentIndent.Count]);
            }

            jsonWriter.WritePropertyName(propertyName);
        }

        public void WriteEnd()
        {
            for (int i = currentIndent.Count - 1; i >= 0; i--)
            {
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
        }
    }
}
