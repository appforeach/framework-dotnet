using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    internal class AuditAsyncQueue : IAuditAsyncQueue
    {
        private readonly Channel<AuditEntity> channel;

        public AuditAsyncQueue()
        {
           channel = Channel.CreateBounded<AuditEntity>(
               new BoundedChannelOptions(10000)
               {
                   FullMode = BoundedChannelFullMode.Wait,
                   SingleReader = true,
                   SingleWriter = false
               });
        }

        public ValueTask QueueAsync(AuditEntity auditEntity)
            => channel.Writer.WriteAsync(auditEntity);

        public ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default)
            => channel.Reader.WaitToReadAsync(cancellationToken);

        public bool TryRead(out AuditEntity auditEntity)
            => channel.Reader.TryRead(out auditEntity);
    }
}
