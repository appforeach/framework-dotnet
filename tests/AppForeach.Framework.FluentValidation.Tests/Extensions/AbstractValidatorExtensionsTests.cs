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
    private class CreateInvoiceCommand { public string Name { get; set; } }
    private class InvoiceEntity { public string Name { get; set; } }

    private class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand> { }

    private class TestMappingMetadata : IMappingMetadata
    {
        public Type DestinationType { get; set; }
        public Type SourceType { get; set; }
        public IEnumerable<IPropertyMap> PropertyMaps { get; set; }
    }

    private class TestPropertyMap : IPropertyMap
    {
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
    }

    private class InvoiceEntitySpecification : BaseEntitySpecification<InvoiceEntity>
    {
        public InvoiceEntitySpecification()
        {
            Field(x => x.Name).IsRequired();
        }
    }

    CreateInvoiceCommandValidator validator = new CreateInvoiceCommandValidator();
    Mock<IMappingMetadataProvider> metadataProviderMock = new Mock<IMappingMetadataProvider>();
    TestPropertyMap propertyMap = new TestPropertyMap { SourceName = "Name", DestinationName = "Name" };
    TestMappingMetadata mappingMetadata;

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
    public void InheritFromMappingAndSpecification_ShouldApplyRequiredFacet()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata });

        // Act
        validator.InheritFromMappingAndSpecification(metadataProviderMock.Object);

        // Assert
        var result = validator.Validate(new CreateInvoiceCommand());

        result.IsValid.ShouldBeFalse();

        //TODO: check property name
        //result.Errors.ShouldContain(e => e.PropertyName == "Name" && e.ErrorMessage.Contains("must not be empty"));
        result.Errors.ShouldContain(e => e.ErrorMessage.Contains("must not be empty"));
    }

    [Fact]
    public void InheritFromMappingAndSpecification_ShouldSkipValidationForSkippedProperties()
    {
        // Arrange
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata });

        // Act
        validator.InheritFromMappingAndSpecification(metadataProviderMock.Object, options => options.Skip(x => x.Name));

        // Assert
        var result = validator.Validate(new CreateInvoiceCommand());
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void InheritFromMappingAndSpecification_ShouldThrowExceptionWhenMultipleSpecificationsFound()
    {
        // Arranges
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(new List<IMappingMetadata> { mappingMetadata, mappingMetadata });

        // Act & Assert
        Should.Throw<MultipleSpecificationsPerCommandFoundException>(() => validator.InheritFromMappingAndSpecification(metadataProviderMock.Object));
    }

    [Fact]
    public void InheritFromMappingAndSpecification_ShouldThrowExceptionWhenNoSpecificationFound()
    {
        // Arrange
        var validator = new CreateInvoiceCommandValidator();
        metadataProviderMock.Setup(m => m.GetMappingMetadata(It.IsAny<Type>())).Returns(Enumerable.Empty<IMappingMetadata>());

        // Act & Assert
        Should.Throw<UnableToMapCommandToSpecificationException>(() => validator.InheritFromMappingAndSpecification(metadataProviderMock.Object));
    }
}
