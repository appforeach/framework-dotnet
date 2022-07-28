# AppForeach Documentation

AppForeach is a full-stack framework with interconnected features spanning all layers of application.

## Architecture overview

Business component execution pipeline is a central part of the system. Design evolution and internal architecture is described in [business scope architecture](business-scope.md) document.


## Features

All stack features table of contents.

- Business component execution
    - [Business scope](business-scope.md)
    - [Logging](logging.md)
    - [Audit](audit.md)
    - [Input validation](input-validation.md)
    - Unit of work
        - [Transaction management](transaction-management.md)
        - [Optimistic concurrency](optimistic-concurrency.md)
        - [Pessimistic concurrency](pessimistic-concurrency.md)
    - [Authorization](authorization.md)
    - [Error handling](error-handling.md)
    - [Metrics](metrics.md)
    - Bussiness support features
        - [Configuration](configuration.md)
        - [Localization](localization.md)
        - [Classifiers](classifiers.md)
    - Hosting
        - [IIS](iis.md)
        - [Windows Service](windows-service.md)
        - [.NET Core and Docker](netcore.md)
    - Activation
        - [ASP.NET](aspnet.md)
        - [MassTransit](masstransit.md)
        - [Scheduled tasks](scheduled-tasks.md)
- Microservices communication
    - [Message routing](message-routing.md)
    - [Inbox and outbox](message-inbox-outbox.md)
    - [Task workflow](task-workflow.md)
    - [State machine workflow](state-workflow.md)
- User interface
    - Common form input controls
    - Business data type
    - Validation
    - Data grid
    - Application shell
        - Session support
        - Window manager
- Administration website
- Code generation and productivity
- Reusable infrastructure services
    - Job service
    - Communication service
    - Blob storage service 
    - Document search service

## Packaging

Framework encourages writing business component code that has no dependencies on framework itself. In addition, recommended [packaging design](packaging.md) ensures that investment in business domain modeling is protected from changes in supportive frameworks or infrastructure.


