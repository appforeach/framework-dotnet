using AppForeach.Framework.Hosting.Features.Logging;
using AppForeach.Framework.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggingPropertyProviderExtensions
    {
        public static void AddApplicationConfigurationLoggingProperty<T>(this IServiceCollection services, 
            string propertyName, string configurationKey, T? defaultValue = default)
        {
            services.AddOptions();

            services.TryAddSingleton<ILoggingPropertyProvider, ConfigurationLoggingPropertyProvider>();

            services.Configure<ConfigurationLoggingPropertyProviderSettings>(opt =>
            {
                opt.Items.Add(new ConfigurationLoggingPropertyProviderOptionItem<T>
                {
                    PropertyName = propertyName,
                    ConfigurationKey = configurationKey,
                    DefaultValue = defaultValue
                });
            });
        }
    }
}
