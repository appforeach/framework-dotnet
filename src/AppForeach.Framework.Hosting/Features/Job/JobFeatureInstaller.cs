using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppForeach.Framework.Hosting.Features.Job;

internal class JobFeatureInstaller<TOperation> 
    (
        JobFeatureOption<TOperation> featureOption
    ) : IApplicationFeatureInstaller
    where TOperation : new()
{
    public void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
    {
        services.Configure<JobFeatureOption<TOperation>>(option =>
        {
            option.Interval = featureOption.Interval;
            option.OperationOptions = opt =>
            {
                opt
                    .HasValidator(false)
                    .TransactionInsertFact(false)
                    .AuditEnabled(false);

                featureOption.OperationOptions?.Invoke(opt);
            };
        });

        services.TryAddSingleton<JobBackgroundService<TOperation>>();
        services.AddHostedService(sp => sp.GetRequiredService<JobBackgroundService<TOperation>>());
    }
}
