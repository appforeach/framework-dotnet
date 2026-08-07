using System.Collections.Generic;

namespace AppForeach.Framework.EntityFrameworkCore.Audit;

public class AuditSaveBatchCommand
{
    public List<AuditEntity> AuditRecords { get; set; }
}
