# Transaction middleware

## Overview

Transaction middleware implements unit of work for business handler code.  

Middleware performs following steps:
- opens database connection;
- starts database transaction;
- creates a fact record in ```framework.Transaction``` table;
- ensures all DbContext instances injected into subsequent code are enlisted in same transaction;
- commits transaction if bussiness code executes without exception.

This way all business code defined in command or query handler is executed reliably within a single transaction.

### Commands and Queries

Transaction middleware behaves differently for Queries and Commands

- Middleware commits transactions only for operations that are considered Commands.
- For Queries transaction is opened but not committed.
- For Queries change tracker behavior is set to `QueryTrackingBehavior.NoTracking`.
- For Queries a call to `DbContext.SaveChanges` will also throw an exception.

### Several DbContext in applicaton

Automatic enlistment of DbContext instances into transaction also means that multiple different DbContext types can be easily used in application if needed.

## Configuration

Middleware supports several configuration settings.

| Setting | Default value | Description |
| --- | --- | --- |
| TransactionIsolationLevel | Snapshot | specifies transaction isolation level |
| TransactionRetry | false | middleware will use `SqlServerRetryingExecutionStrategy` if this flag is set  |
| TransactionRetryCount | 3 | maximum number of attempts |
| TransactionMaxRetryDelay | 30 sec | maximum delay between retries | 

In case of transaction retry new dependency injection scope is created before handler execution. This ensures that new retry is using fresh dependencies.

## Custom Transaction Retry

Transaction middleware supports manually controlled custom retries. Following steps are needed for custom retry scenario:

- throw custom exception in code;
- implement `AppForeach.Framework.EntityFrameworkCore.ITransactionRetryExceptionHandler` and register it in application container; 
- handler method `ShouldRetryTransaction` must return true to indicate that transaction is to be retried.

