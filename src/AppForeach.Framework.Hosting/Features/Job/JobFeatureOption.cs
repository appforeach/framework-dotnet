using System;

namespace AppForeach.Framework.Hosting.Features.Job;

public class JobFeatureOption<TOperation> : IApplicationFeatureOption
    where TOperation : new()
{
    public IApplicationFeatureInstaller Installer => new JobFeatureInstaller<TOperation>(this);

    public TimeSpan Interval { get; set; }

    public Action<IOperationBuilder>? OperationOptions { get; set; }
}
