using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSagas.Infrastructure
{
    public class BaseActivity<TState>
    {
        protected IStepOptions<TState> Initial()
        {
            return null;
        }

        protected IStepOptions<TState> Step(string stepName)
        {
            return null;
        }
    }
}
