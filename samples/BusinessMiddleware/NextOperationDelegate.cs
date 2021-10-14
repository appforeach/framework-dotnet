using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessMiddleware
{
    public delegate Task NextOperationDelegate(IOperationContext context);
}
