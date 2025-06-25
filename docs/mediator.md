# Mediator

## Mediator overview

Mediator reduces dependencies between components and allows execution of non-functional code.

Mediator implementation provides following features

- Handler resolution based on input object
- Middlewares execution between call to mediator and handler execution
- Middleware configuration possibilities.

```
IOperationMediator operationMediator.Execute(MyCommand command)
    middleware1.ExecuteAsync(NextOperationDelegate next)
        middleware2.ExecuteAsync(NextOperationDelegate next)
            MyHandler handler.Handle(MyCommand command)
```

These middlewares implement common non-functional requirements such as [validation](mediator-validation.md) or [transaction](mediator-unit-of-work) management.


## Operation execution

Operation must be executed by passing input data transfer object into `IOperationMediator.Execute` method.

```C#
var inputDto = new MyCommandOrQuery();
await operationMediator.Execute(inputDto);
```

### Handler resolution

Framework provides default handler resolution. Operation mediator finds correspending Command or Query handler based on input data object type by the following convention:

- input data type name must end with either **Command** or **Query**, i.e. CreateMyEntityCommand;
- handler type name must end with **Handler**, i.e. MyCreateEntityHandler;
- handler type must have public method with first parameter type that matches provided input object.

Command and Query handlers are registered in Dependency Injection container automatically when adding pipeline to application by:

```C#
services.AddMediator();
```

### Operation name

Both commands and queries have a user friendly operation name that is used in audit or authorization scenarios.

Operation name is taken from input data object type name by removing suffix by default.

| Input data type name | Operation name |
| --- | --- |
| CreateInvoiceCommand | CreateInvoice |
| GetInvoiceByIdQuery | GetInvoiceById |

Operation name can be specified manually when calling operation through mediator.

```C#
operationMediator.Execute(input, options => options.OperationName("MyCustomOperationName"))
```

### Commands and Queries

Mediator enforces segregation of operations into Commands and Queries:

- operation is Command if input type name ends with **Command**;
- operation is Query if input type name ends with **Query**;

Middlewares might have different behavior based on this segregation. For example, TransactionMiddleware will set `QueryTrackingBehavior.NoTracking` for Queries and will not commit transaction.

## Operation result

Operation execution result will be returned as `OperationResult` data object. Notable fields are:

| Field | Description |
| --- | --- |
| OperationOutcome Outcome | enumeration specifies execution status - Success, Error or Partial |
| object Result | contains handler method return value
| List\<OperationIssue\> Errors | may contain validation or other errors | 

It is recommended to use extension method to map `OperationResult` to application specific result, for example:

```C#
return operationMediator.Execute(input).ToApiResponse<MyCommandResponse>();
```

This may be used to map errors to HTTP Problem Details in Web Api scenarios.

## Middleware configuration

Middlewares can accept different configuration settings. Configuration to middleware can be provided at different levels.

### Enterprise level

Configuration at enterprise level is default middleware configuration for all microservices in organization. It is typically performed at microservice chasis project that is referenced by all microservices.

### Microservice level

Configuration at microservice level is middleware configuration for all handlers in particual application. Settings at application level can be specified by options builder when registerting mediator:

```C#

services.AddMediator(op => op.MiddlewareSetting(settingValue));

```

### Operation level

Configuration at particular operation level can be provided when calling operation through mediator.

```C#
operationMediator.Execute(command, options => options.MiddlewareSetting(settingValue))
```



