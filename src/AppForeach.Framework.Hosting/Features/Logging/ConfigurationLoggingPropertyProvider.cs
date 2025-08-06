using AppForeach.Framework.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    public class ConfigurationLoggingPropertyProvider : ILoggingPropertyProvider
    {
        private readonly IOptions<ConfigurationLoggingPropertyProviderSettings> options;
        private readonly IConfiguration configuration;
        private Dictionary<string, object>? properties;

        public ConfigurationLoggingPropertyProvider(IOptions<ConfigurationLoggingPropertyProviderSettings> options,
            IConfiguration configuration) 
        {
            this.options = options;
            this.configuration = configuration;
        }

        public Dictionary<string, object> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = CombineProperties();
                }

                return properties;
            }
        }

        private Dictionary<string, object> CombineProperties()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach(var item in options.Value.Items)
            {
                var value = item.GetValue(configuration);

                if (value != null)
                {
                    result.Add(item.PropertyName, value);
                }
            }

            return result;
        }
    }
}
