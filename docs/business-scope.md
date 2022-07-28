# Business scope pipeline

Important part of modern custom developed software is a set of non-functional requirements like security checks and audit. Additional quality attributes include transaction management, validation, reliable communication and metrics. This functionality  does not relate to any business process but is neeeded to support application ecosystem as a whole. 

Implementation of such cross-cutting concerns mostly does not change from one bussiness process to another. Main challenge framework solves is reusability of non-functional quality assets across application while keeping minimal impact on business features.

## Initial design

One of the first attempts to separate and reuse cross-cutting concerns was made by introducing operation scope in a form of `BusinessScope : IDisposable`. 

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

This bisness scope class had operation name as an input and incapsulated several important aspects:
- database transaction management;
- operation logging and auditing;
- authorization checks.

Being a step forward, design still required additional code like `using` construct and explicit call to `BusinessScope.Complete()` to mark transaction as successfull.

## Business middlewares

A natural evolution of initial approach is introduction of business middlewares. Each business middleware implements `IOperationMiddleware` interface so it cane be easily plugged into common pipeline.

```C#
    public interface IOperationMiddleware
    {
        Task ExecuteAsync(NextOperationDelegate next);
    }
```

This single interface allows to define aspects that run before and after each business operation. Each middleware can also use framework registered services and context `IOperationContext`.

```C#
    public class SampleMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;

        public SampleMiddleware(IOperationContext context)
        {
            this.context = context;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            // some code before operation

            await next();

            // and some after
        }
    }
```

## Activation 

A set of middlewares defined into single pipeline implement most of non-functional requirements. Final needed piece is one-liner method to expose business operation as an endpoint, either as Web API, service bus consumer or some other protocol like gRPC.

```C#
Consume<CreateInvoiceRequest>();
```

Mediator based activation is supported possible and might be prefered approach in some cases.

```C#
operationMediator.Execute(createInvoiceRequest);
```

Both activations are a starting point to execute operation. Framework finds corresponding business operation handler class and processes business request along with all configured middlewares.

## Operation name

Operation name is vital for several concerns like security and logging. Framework uses `IOperationNameResolver` to implicitely resolve operation name from input data object type and/or handler type.

```C#
typeof(Organization.Invoice.Commands.CreateInvoiceCommand) => "CreateInvoice";
```

## Business feature code

Moving business scope out of business code into related middlewares make resulting business code as clean as possible. 

```C#
public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
{
    Invoice invoice = mapping.Map(request);

    int invoiceId = invoiceRepository.Create(invoice);

    return new CreateInvoiceResponse(invoiceId);
}
```

## Middleware configuration

Sometimes behavior middleware provides needs to be adjusted or even turned off entirely. Framework provides a way to pass any arbitary facets into middleware exacution pipeline.

For example, if default operation name resolution does not fit particular scenario, it can be adjusted via `IOperationBuilder` for any particular operation.

```C#
Consume<CreateInvoiceInput>(opt => opt.OperationName("SomeSpecific"));
```

Same approach works with mediator style activation.

```C#
operationMediator.Execute(createInvoiceRequest, opt => opt.OperationName("SomeSpecific"));
```

It is also possible to configure middlewares at application level for all operations. This is possible using same `IOperationBuilder` extensibility mechanism.

```C#
services.AddAppForeach(s => 
{
    s.OperationConfiguration(opt => opt.TransactionIsolation(IsolationLevel.Serializable));
});
```

