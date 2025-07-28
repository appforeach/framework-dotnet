# Entity Specification

## Overview

In application developed according to Domain Driven Design, CQRS and Clean Architecture principes it is needed to specify Entity field metadata at Infrastructure level - EF Core configuration. More of this, almost the same field metadata is used to validate incoming requests, especially, Create and Update commands. This results in a lot of repeated code that is tedious to write and difficult to maintain.

Entity specification feature aims to reduce amount of code needed to configure both EF Core metadata and input validation.

To use this feature create and define Specification artifacts next to Entity classes. Use Fluent syntax to define field metadata.

```C#
// Contoso.Invoice.Domain project

// Entities\InvoiceEntity.cs

public class InvoiceEntity
{
    public string InvoiceNumber { get; set; }
}

// Specification\InvoiceEntitySpecification.cs

public class InvoiceEntitySpecification : BaseSpecification<InvoiceEntity>
{
    public InvoiceEntitySpecification()
    {
        Field(e => e.InvoiceNumber).IsRequired(true).MaxLength(100);
    }
}
```

Then use specification helper to populate EF Core metadata.

```C#
// Contoso.Invoice.Persistance project

// EntityConfiguration\InvoiceEntityConfiguration.cs

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.InheritFromEntitySpecification();
    }
}
```

Next use validation helper to populate validation rules for i.e. FluentValidation validator. Note that incomming command or query objects are defined using separate artifacts. So, mapping metadata from i.e. AutoMapper is used to populate validation rules for particular fields if they are mapped.

```C#
// Contoso.Invoice.Application project

// Commands\CreateInvoice\CreateInvoiceCommandMapping.cs

public class CreateInvoiceCommandMapping : Profile
{
    public CreateSampleMapping()
    {
        CreateMap<CreateInvoiceCommand, InvoiceEntity>();
    }
}

// Commands\CreateInvoice\CreateInvoiceCommandValidator.cs

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        this.InheritFromEntitySpecification();
    }
}
```

Same approach is applicable to all other Commands or Queries that map a lot of fields to target Entity, like UpdateInvoiceCommand.

## Default configuration for primitive types

It is possible to specify some default metadata for primitive types, such as ```string``` or ```decimal```. This default type metadata can be defined at Enterprise level - base assembly for all microservices.

```C#
// Contoso.MicroserviceBase.Domain project

// ContosoBaseSpecification.cs

public class ContosoBaseSpecification<TType> : BaseEntitySpecification<TType>
{
    public ContosoBaseSpecification()
    {
        Type<string>().MaxLength(50);
    }
}
```

Then optionally adjusted at particular microservice level. Overriden metadata settings at lower inheritance levels will take precedence.

```C#
// Contoso.Invoice.Domain project

// Specification\InvoiceBaseSpecification.cs

public class InvoiceBaseSpecification<TType> : ContosoBaseSpecification<TType>
{
    public InvoiceBaseSpecification()
    {
        Type<string>().MaxLength(20);
    }
}
```

Inherit particular entity specification definition from base specification to apply this default settings.

```C#

// Contoso.Invoice.Domain project

// Specification\InvoiceEntitySpecification.cs

public class InvoiceEntitySpecification : InvoiceBaseSpecification<InvoiceEntity>
{
    public InvoiceEntitySpecification()
    {
    }
}
```

Particular field metadata settings will take precedence over defined default metadata for primitive types.

## Override specification configuration

Metadata defined in specification can be overriden at any layer if needed.

To override metadata at EF Core level use native EF Core metadata definition interface after a call to specification helper.

```C#
// Contoso.Invoice.Persistance project

// EntityConfiguration\InvoiceEntityConfiguration.cs

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.InheritFromEntitySpecification();

        builder.Property(e => e.InvoiceNumber).HasMaxLength(10);
    }
}
```

Same approach can be used for validation, using native validation syntax, for example, as of FluentValidation if it is used.

```C#
// Contoso.Invoice.Application project

// Commands\CreateInvoice\CreateInvoiceCommandValidator.cs

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        this.InheritFromEntitySpecification();

        RuleFor(e => e.InvoiceNumber).MaximumLength(10);        
    }
}
```

## Available metadata settings

### IsRequired and IsOptional

```IsRequired``` and ```IsOptional``` metadata settings define field nullability / requirement. Settings are applicable to all types.

At EF Core level are mapped to ```IsRequired``` property metadata if field is required.

At validation level are mapped to ```NotNull``` validator if field is required (as for FluentValidation).

Any setting of ```NotNull```, ```Null```, ```NotEmpty```, ```Empty``` FluentValidation validators will make metadata in specification insignificant.

### IsEmptyAllowed

```IsEmptyAllowed``` metadata setting is applicable to ```string``` type only. This setting define if field accepts empty or whitespace values.

At EF Core level this setting has no effect.

Any setting of ```NotNull```, ```Null```, ```NotEmpty```, ```Empty``` FluentValidation validators will make metadata in specification insignificant.

At validation level, for FluentValidation, result is based on combination of ```IsRequired``` \ ```IsOptional``` and ```IsEmptyAllowed``` conbination.

| IsRequired | IsEmptyAllowed | FluentValidation validator | Comment |
| true | true | NotNull | |
| true | false | NotEmpty | |
| false | true | - | nothing applied |
| false | false | custom | pass nulls but not empty or whitespace |

Setting ```IsEmptyAllowed``` false as default at type level will improve data quality by guarding against empty or whitespace everywhere in all microservices or particular microservice.

### HasMaxLength

```HasMaxLength``` metadata settings apply to ```string``` only and define field maximum length.

At EF Core level are mapped to ```HasMaxLength``` property metadata.

At validation level are mapped to ```MaxLength``` validator (as for FluentValidation).

Setting of ```MaxLength``` FluentValidation validator will make metadata in specification unimportant.

Setting ```HasMaxLength``` with some default value at type level will provide some baseline for strings in all microservices or particular microservice.

### HasPrecision

```HasPrecision``` metadata settings apply to ```decimal``` type only and define field precision.

At EF Core level are mapped to ```HasPrecision``` property metadata.

At validation level are mapped to ```PrecisionScale``` validator (as for FluentValidation).

Setting of ```PrecisionScale``` FluentValidation validator will make metadata in specification unimportant.

Setting ```HasPrecision``` with some default value at type level at enterprise or microservice levels will provide some baseline for decimals in all microservices or particular microservice, respectfully.




