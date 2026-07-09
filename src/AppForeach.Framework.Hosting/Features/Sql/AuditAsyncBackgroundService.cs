using AppForeach.Framework.EntityFrameworkCore.Audit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Features.Sql;

public class AuditAsyncBackgroundService 
    (
    IAuditAsyncProcessingService auditAsyncProcessing
    ) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        => auditAsyncProcessing.ProcessAll(stoppingToken);
}
