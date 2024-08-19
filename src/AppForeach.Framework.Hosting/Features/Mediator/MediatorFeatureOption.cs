using System;
using System.Collections.Generic;

namespace AppForeach.Framework.Hosting.Features.Mediator
{
    public class MediatorFeatureOption : IApplicationFeatureOption
    {
        public IApplicationFeatureInstaller Installer => new MediatorFeatureInstaller(this);

        public Action<IOperationBuilder>? ApplicationOptions { get; set; }

        public Func<bool, List<Type>>? GetMiddlewares { get; set; }
    }
}
