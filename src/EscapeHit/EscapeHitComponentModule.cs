using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.FluentValidation;

namespace EscapeHit
{
    public class EscapeHitComponentModule : FrameworkModule
    {
        public EscapeHitComponentModule()
        {
            DefaultHandlerRegistration();
            DefaultEntitySpecificationRegistration();

            componentScanners.Add(new FluentValidatorScanner());
        }
    }
}
