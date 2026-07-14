using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class CompiledHandlerInfo
    {
        public Func<object, object, CancellationToken, Task<object>> HandlerFunc { get; set; }

        public Type HandlerType { get; set; }
    }
}
