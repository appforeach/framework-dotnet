using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    public class ConfigurationLoggingPropertyProviderSettings
    {
        public List<IConfigurationLoggingPropertyProviderOptionItem> Items { get; } = new();
    }
}
