using System.Threading.Tasks;

namespace BusinessMiddleware
{
    public class SampleMiddleware : IOperationMiddleware
    {
        private readonly IFrameworkService frameworkService;

        public SampleMiddleware(IFrameworkService frameworkService)
        {
            this.frameworkService = frameworkService;
        }

        public async Task ExecuteAsync(IOperationContext context, NextOperationDelegate next)
        {
            // some code before operation

            await next(context);

            // and some after
        }
    }
}
