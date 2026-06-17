using System;

namespace AppForeach.Framework.Hosting.Features.Job;

public class JobOptions<TOperation>
    where TOperation : new()
{
    public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(60);

    public bool Enabled { get; set; } = true;

    public Action<IOperationBuilder>? OperationOptions { get; set; }
}
