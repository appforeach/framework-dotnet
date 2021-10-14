using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalBusinessScope
{
    public class BusinessScope : IDisposable
    {
        public BusinessScope(Operation operation, object input)
        {

        }

        public void Dispose()
        {
        }

        public void Complete();
    }
}
