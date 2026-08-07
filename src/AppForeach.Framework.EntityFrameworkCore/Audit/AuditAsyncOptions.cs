using System.Threading.Channels;

namespace AppForeach.Framework.EntityFrameworkCore.Audit;

public class AuditAsyncOptions
{
    public int QueueCapacity { get; set; } = 100000;

    public BoundedChannelFullMode QueueFullMode { get; set; } = BoundedChannelFullMode.Wait;

    public int WriteBatchSize { get; set; } = 300;
}
