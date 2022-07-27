using AppForeach.Framework.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class ValidationMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly IValidatorMap validatorMap;
        private readonly IServiceLocator serviceLocator;
        private readonly IValidationFailedEventHandler validationFailedEventHandler;

        public ValidationMiddleware(IOperationContext context, IValidatorMap validatorMap, IServiceLocator serviceLocator, IValidationFailedEventHandler validationFailedEventHandler)
        {
            this.context = context;
            this.validatorMap = validatorMap;
            this.serviceLocator = serviceLocator;
            this.validationFailedEventHandler = validationFailedEventHandler;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var validationFacet = context.Configuration.TryGet<ValidationHasValidatorFacet>();

            if(validationFacet?.HasValidator ?? true)
            {
                Type inputType = context.Input.GetType();
                Type validatorType = validatorMap.GetValidatorType(inputType);

                if(validatorType == null)
                {
                    throw new FrameworkException($"Validator type not found for input of type { inputType }.");
                }

                IValidator validator = serviceLocator.GetService(validatorType) as IValidator;

                var validationResult = validator.Validate(context.Input);

                if(validationResult.Outcome != OperationOutcome.Success || validationResult.Errors.Count > 0)
                {
                    var outputState = context.State.Get<OperationOutputState>();
                    var result = outputState.Result;

                    result.Errors = validationResult.Errors;
                    result.Outcome = OperationOutcome.Error;

                    validationFailedEventHandler.OnValidationFailed(result);
                    return;
                }
            }

            await next();
        }
    }
}
