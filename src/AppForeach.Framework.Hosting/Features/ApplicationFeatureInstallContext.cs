using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features
{
    public class ApplicationFeatureInstallContext : IApplicationFeatureInstallContext
    {
        public required IEnumerable<IApplicationFeatureOption> Options { get; set; }

        public required IConfigurationManager Configuration { get; set; }
    }
}
