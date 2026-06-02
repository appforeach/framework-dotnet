using System;

namespace AppForeach.Framework.Hosting.Features.Job;

public class JobOptions<TOperation>
    where TOperation : new()
{
    public TimeSpan Interval { get; set; }

    public Action<IOperationBuilder>? OperationOptions { get; set; }
}
