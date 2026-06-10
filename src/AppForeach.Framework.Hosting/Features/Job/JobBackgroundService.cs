using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Features.Job;

public class JobBackgroundService<TOperation>
    (
    IServiceScopeFactory scopeFactory,
    IOptionsMonitor<JobOptions<TOperation>> optionsMonitor,
    ILogger<JobBackgroundService<TOperation>> logger
    ) : BackgroundService
    where TOperation : new()
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var start = DateTime.UtcNow;
            var options = optionsMonitor.CurrentValue;

            if (options.Enabled)
            {

                await DoWorkAsync(options, stoppingToken);

                var remainingWait = options.Interval - (DateTime.UtcNow - start);

                if (remainingWait > TimeSpan.Zero)
                {
                    await Task.Delay(remainingWait, stoppingToken);
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }

    private async Task DoWorkAsync(JobOptions<TOperation> options, CancellationToken cancellationToken)
    {
        var activity = new Activity(typeof(TOperation).Name + " Job");
        activity.Start();

        try 
        {
            await using var scope = scopeFactory.CreateAsyncScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IOperationMediator>();

            var result = await mediator.Execute(new TOperation(), options.OperationOptions, cancellationToken);

            if(result.Outcome != OperationOutcome.Success)
            {
                activity.SetStatus(ActivityStatusCode.Error, "Operation failed");
            }
        }
        catch(Exception ex)
        {
            activity.SetStatus(ActivityStatusCode.Error, ex.Message);
            logger.LogError(ex, "Unexpected excetion during job mediator invocation.");
        }
        finally
        {
            activity.Stop();
        }
    }
}
