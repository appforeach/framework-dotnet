
namespace AppForeach.Framework.EntityFrameworkCore;

public class TransactionCleanupOptions
{
    public int RetentionDays { get; set; } = 30;

    public int BatchSize { get; set; } = 1000;
}
