using Microsoft.Extensions.Configuration;

namespace AppForeach.Framework.Hosting.Features.Logging
{
    public interface IConfigurationLoggingPropertyProviderOptionItem
    {
        string PropertyName { get; }

        object? GetValue(IConfiguration configurtion);
    }
}
