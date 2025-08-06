
using System;

namespace AppForeach.Framework.Hosting.Startup
{
    public class ApplicationStartupDescriptor<TImplementation> : IApplicationStartupDescriptor
        where TImplementation : IApplicationStartup
    {
        public Type ImplemenationType => typeof(TImplementation);

        public ApplicationStartupOptions? Options { get; set; }
    }
}
