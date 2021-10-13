# AppForeach Framework

AppForeach is open-source reference implementation of microservices architecture for .NET Platform. Main design goals are unobstructive cross-cutting concerns and quality of service attributes of resulting software with emphasys on developer productivity.

## Overview

Framework allows to create a business operation definition that is clean from any infrastructure services and is easily unit testable.

```C#
public class CreateInvoiceCommand
{
    // ... constructor and services injection ...

    public async Task<CreateInvoiceOutput> Execute(CreateInvoiceInput input)
    {
        var entity = inputMapping.MapFrom(input);

        entity.Number = invoiceNumberService.GenerateInvoiceNumber(entity);

        await invoiceRepository.Create(entity);

        return outputMapping.MapFrom(entity);
    }
}
```

Such component can now be now easily exposed as an endpoint, for example as a regular ASP.NET Web API method.

```C#
public class InvoiceController : WebApiController
{
    [HttpPost]
    public async Task<ObjectResult> Create(CreateInvoiceInput input)
    {
        return await operationMediator.Execute(input).Ok();
    }
}
```

Alternatively same component can be registered to listen to service bus messages.

```C#
public class InvoiceMessageHost : MessageHost
{
    public InvoiceMessageHost()
    {
        Consume<CreateInvoiceInput>();
    }
}
```

In both cases framework executes a set of pipelined middlewares between endpoint definition and actual business component activation. This enriches resulting software with needed non-functional features like logging, audit, authorization and unit of work management.

## Documentation

Several documents describe main features of the framework.

AppForeach is a full-stack framework. Several documents describe main features of the software.



## Business scope

Business component execution pipeline is a central part of the framework. Design evolution and internal architecture is described in **business scope architecture** document.



Framework encourages writing business component code that has no dependencies on framework itself. In addition, recommended **packaging design** ensures that investment in business domain modeling is protected from changes in supportive frameworks or infrastructure.

## Microservices communication

