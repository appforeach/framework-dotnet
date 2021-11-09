using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class TransactionEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset OccuredOn { get; set; }

        public string Host { get; set; }
    }
}
