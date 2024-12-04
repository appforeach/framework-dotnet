﻿using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Validation;
using FluentValidation.Results;

namespace AppForeach.Framework.FluentValidation
{
    public class FluentValidatorProxy<TAbstractValidator> : IValidator
    {
        private readonly IServiceLocator serviceLocator;

        public FluentValidatorProxy(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public OperationResult Validate(object input)
        {
            Type fluentValidatorType = typeof(TAbstractValidator);
            var fluentValidator = serviceLocator.GetService(fluentValidatorType);

            var method = fluentValidatorType.GetMethod("Validate", new Type[] { input.GetType() })
                ?? throw new FrameworkException($"{ fluentValidatorType } does not contain expected method Validate.");

            var fluentValidatorResult = (method.Invoke(fluentValidator, new object[] { input }) as ValidationResult)
                ?? throw new FrameworkException($"{fluentValidatorType} does not contain expected method Validate."); ;

            var operationResult = new OperationResult();

            if (fluentValidatorResult.IsValid)
            {
                operationResult.Outcome = OperationOutcome.Success;
            }
            else
            {
                operationResult.Outcome = OperationOutcome.Error;

                operationResult.Errors = fluentValidatorResult.Errors.Select(v => new OperationIssue
                {
                    Field = v.PropertyName,
                    Code = v.ErrorCode,
                    Message = v.ErrorMessage
                }).ToList();
            }

            return operationResult;
        }
    }
}
