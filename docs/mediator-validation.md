# Validation middleware

## Overview

Validation middleware performs specified input data object checks and automatically returns list of errors if issues are found.

## Usage

Framework provided validation middleware is independant of particular validation library.

Framework provides support for FluentValidation.

Middleware automatically finds corresponding FluentValidation class based on input data object type:

```C#
public class MyCommandValidator : AbstractValidator<MyCommand>
{
    public MyCommandValidator()
    {
        RuleFor(e => e.MyField) /*...*/
    }
}
```

Validators are registered in dependency injection container automatically when registering mediator, in same way like it is done for command and query handlers.

## Configuration

By default missing validator for a data input will be considered an error and handler execution will not be performed. This behavior can be amended at microservice or operation level by middleware configuration setting:

```C#
options => options.HasValidator(false)
```