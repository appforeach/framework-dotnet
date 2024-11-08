using AppForeach.Framework.EntityFrameworkCore.Audit;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public static class AuditOperationBuilderExtensions
    {
        public static IOperationBuilder AuditEnabled(this IOperationBuilder builder, bool enabled)
        {
            var facet = new AuditEnabledFacet
            {
                AuditEnabled = enabled
            };
            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
