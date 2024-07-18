using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartupDescriptor
    {
        Type ImplemenationType { get; }
    }
}
