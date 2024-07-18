using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features
{
    public interface IApplicationFeatureInstallContext
    {
        IEnumerable<IApplicationFeatureOption> Options { get; }

        IConfigurationManager Configuration { get; }
    }
}
