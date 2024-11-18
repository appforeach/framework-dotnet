using AppForeach.Framework.DependencyInjection;

namespace EscapeHit
{
    public class EscapeHitComponentModule : FrameworkModule
    {
        public EscapeHitComponentModule()
        {
            DefaultHandlerRegistration();
            DefaultEntitySpecificationRegistration();
        }
    }
}
