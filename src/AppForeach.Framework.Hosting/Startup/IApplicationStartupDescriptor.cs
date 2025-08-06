using System;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartupDescriptor
    {
        Type ImplemenationType { get; }

        ApplicationStartupOptions? Options { get; }
    }
}
