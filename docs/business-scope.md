# Business scope pipeline

Important part of modern customly developed software is a set of non-functional features. These functionality does not relate to any business process but is neeeded to support important regulatory requirements or to improve overall quality of application ecosystem. Implementation of these cross cutting concerns is mostly straight forward and does not change from one bussiness process to another. Main question is how to reuse this functionality across application while keeping minimal impact on business related code.

## Initial design

One of the first attempt to solve problem of separating and reusing cross-cutting concern related code was made by introducing operation scope in a form of `BusinessScope : IDisposable`. 

```C#
public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
{
    using(var scope = new BusinessScope(Operation.CreateInvoice, request))
    {
        validator.Validate(request);

        Invoice invoice = mapping.Map(request);

        int invoiceId = invoiceRepository.Create(invoice);
        
        var response = new CreateInvoiceResponse(invoiceId);
        
        scope.Complete();
        return response;
    }
}
```

This scope construct had operation name specified by enumeration and incapsulated several important aspects:
- operation logging and auditing;
- authorization routines;
- database transaction management.

Being a step forward, this design still required additional `using` construct and explicit call to `BusinessScope.Complete()` to mark transaction as successfull. Internal design was also not ideal and did not support any extensibility.

## Business middlewares

A natural evolution of initial approach is introduction of business middlewares. Each business middleware unit implements `IOperationMiddleware` interface to be easily plugged into common pipeline.

```C#
public interface IOperationMiddleware
{
    Task ExecuteAsync(IOperationContext context, NextOperationDelegate next);
}
```

This single interface allows to define aspects that run before and after each business operation. Each middleware can also use framework registered services and access `IOperationContext`.

```C#
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
```
A set of middlewares defined into single pipeline allows to implement most of non-functional requirements. Final needed piece is one-liner method to expose business operation as an endpoint, either as Web API, service bus consumer or some other protocol like gRPC.

```C#
Consume<CreateInvoiceRequest>();
```

This endpoint defintion is a starting pointing to activate operation. Framework finds corresponding business operation defintion class and processes business request. Resulting simplified sample call stack is approximately the following:

```C#
AppForeach.MassTransit.MassTransitConsumerActivator.Initialize()
    // prepares operation input and gathers protocol specific properties
	AppForeach.BusinessScopeFactory.StartScope()  
    // creates business operation scope, at this point flow can be reused accross different protocols
	AppForeach.BusinessScope.RunMiddlewares()
    // prepares and executes configured business pipeline
		AppForeach.Logging.AuditMiddleware.Execute()
			// logs operation start, optionally saving entire request for audit purposes
			AppForeach.Validation.ValidationMiddleware.Execute()
				//finds corresponding operation validator
				Organization.Domain.Operation.OperationValidator.Validate() 
					// <-- actual business operation validator -->
					AppForeach.UnitOfWorkMiddleware.Execute()
						// starts unit of work, opens database transaction
						AppForeach.BusinessOperationActivator.Execute()
							Organization.Domain.Operation.OperationCode.Handle()
							// <-- actual business component code
						// completes unit of work, commits transaction
			// logs operation completion
	AppForeach.BusinessScope.Dispose()
AppForeach.MassTransit.MassTransitConsumerActivator.Dispose()
```

> **NOTE:** Some of middlewares not shown in previous stack that are commonly used are **authorization** related middleware and **metrics** feature to collect time elapsed while process request.

Operation name is vital for several concerns like security and logging. Framework uses `IOperationNameResolver` to implicitely resolve operation name.

```C#
typeof(Organization.Invoice.Commands.CreateInvoiceCommand) => "CreateInvoice";
```

If default operation name resolution does not fit particular scenario, it can be adjusted via `IOperationPipelineBuilder`.

```C#
Consume<CreateInvoiceInput>().OperationName("SomeSpecificInvoiceOperation");
```

This operation specification builder is a mechanism to define some custom behavior for middlewares for a single operation , for example, to turn of automatic database transaction, or to adjust logging if needed.

Moving business scope out of business code, automatic call to corresponding request validator and implicit resolution of operation name all together make resulting business code as clean as possible. 

```C#
public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
{
    Invoice invoice = mapping.Map(request);

    int invoiceId = invoiceRepository.Create(invoice);

    return new CreateInvoiceResponse(invoiceId);
}
```