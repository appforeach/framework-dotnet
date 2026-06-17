
namespace AppForeach.Framework.EntityFrameworkCore.Audit;

public class AuditCleanupOptions
{
    public int RetentionDays { get; set; } = 30;

    public int BatchSize { get; set; } = 1000;
}
