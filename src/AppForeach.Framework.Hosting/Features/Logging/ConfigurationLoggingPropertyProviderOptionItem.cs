using Microsoft.Extensions.Configuration;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    internal class ConfigurationLoggingPropertyProviderOptionItem<T> : IConfigurationLoggingPropertyProviderOptionItem
    {
        public required string PropertyName { get; set; }

        public required string ConfigurationKey {  get; set; }

        public T? DefaultValue { get; set; }

        public object? GetValue(IConfiguration configurtion)
        {
            return configurtion.GetValue<T>(ConfigurationKey) ?? DefaultValue;
        }
    }
}
