using AppForeach.Framework.DataType;
using AppForeach.Framework.FluentValidation.Exceptions;
using AppForeach.Framework.FluentValidation.Extensions;
using AppForeach.Framework.Mapping;
using FluentValidation;
using Moq;
using Shouldly;

namespace AppForeach.Framework.FluentValidation.Tests.Extensions;

public class AbstractValidatorExtensionsTests
{
    private class CreateInvoiceCommand { public required string Name { get; set; } }
    private class InvoiceEntity { public required string Name { get; set; } }

    private class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand> { }

    private class SourceVaidator : AbstractValidator<CreateInvoiceCommand> { }

    private class SourceVaidatorWithOverridenName : AbstractValidator<CreateInvoiceCommand>
    {
        public SourceVaidatorWithOverridenName()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    private class TestMappingMetadata : IMappingMetadata
    {
        public required Type DestinationType { get; set; }
        public required Type SourceType { get; set; }
        public required IEnumerable<IPropertyMap> PropertyMaps { get; set; }
    }

    private class TestPropertyMap : IPropertyMap
    {
        public required string SourceName { get; set; }
        public required string DestinationName { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "This type is used in reflection activities")]
    private class InvoiceEntitySpecification : BaseEntitySpecification<InvoiceEntity>
    {
        public InvoiceEntitySpecification()
        {
            Field(x => x.Name).IsRequired();
            Field(x => x.Name).MaxLength(25);
        }
    }

    readonly CreateInvoiceCommandValidator validator = new CreateInvoiceCommandValidator();
    readonly SourceVaidator emptySourceValidator = new SourceVaidator();
    readonly Mock<IMappingMetadataProvider> metadataProviderMock = new Mock<IMappingMetadataProvider>();
    readonly TestPropertyMap propertyMap = new TestPropertyMap { SourceName = "Name", DestinationName = "Name" };
    readonly TestMappingMetadata mappingMetadata;

    public AbstractValidatorExtensionsTests()
    {
        mappingMetadata = new TestMappingMetadata
        {
            DestinationType = typeof(InvoiceEntity),
            SourceType = typeof(CreateInvoiceCommand),
            PropertyMaps = new List<IPropertyMap> { propertyMap }
        };
    }

    [Fact]
    public void InheritOtherRulesFromSpecification_ShouldApplyRequiredFacet_and_SucceedValidation()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata });

        // Act
        validator.InheritOtherRulesFromSpecification(emptySourceValidator, metadataProviderMock.Object);

        // Assert
        var result = validator.Validate(new CreateInvoiceCommand() { Name = "Test Invoice Command" });

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void InheritOtherRulesFromSpecification_ShouldApplyRequiredFacet_and_FailValidation()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata });

        // Act
        validator.InheritOtherRulesFromSpecification(emptySourceValidator, metadataProviderMock.Object);

        // Assert
        var result = validator.Validate(new CreateInvoiceCommand() { Name = null! });

        result.IsValid.ShouldBeFalse();

        //TODO: check property name
        //result.Errors.ShouldContain(e => e.PropertyName == "Name" && e.ErrorMessage.Contains("must not be empty"));
        result.Errors.ShouldContain(e => e.ErrorMessage.Contains("must not be empty"));
    }

    [Fact]
    public void InheritOtherRulesFromSpecification_ShouldSkipValidationForSkippedProperties()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata });

        // Act
        validator.InheritOtherRulesFromSpecification(new SourceVaidatorWithOverridenName(), metadataProviderMock.Object);

        // Assert
        var result = validator.Validate(new CreateInvoiceCommand() { Name = "Test Invoice Command Very Large Text" });
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void InheritOtherRulesFromSpecification_ShouldThrowExceptionWhenMultipleSpecificationsFound()
    {
        // Arranges
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata, mappingMetadata });

        // Act & Assert
        Should.Throw<MultipleSpecificationsPerCommandFoundException>(() => validator.InheritOtherRulesFromSpecification(emptySourceValidator, metadataProviderMock.Object));
    }

    [Fact]
    public void InheritOtherRulesFromSpecification_ShouldThrowExceptionWhenNoSpecificationFound()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(Enumerable.Empty<IMappingMetadata>());

        // Act & Assert
        Should.Throw<UnableToMapCommandToSpecificationException>(() => validator.InheritOtherRulesFromSpecification(emptySourceValidator, metadataProviderMock.Object));
    }
}
