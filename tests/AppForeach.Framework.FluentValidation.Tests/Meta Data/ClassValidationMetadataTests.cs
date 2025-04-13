using AppForeach.Framework.FluentValidation.Meta_Data;
using Shouldly;

namespace AppForeach.Framework.FluentValidation.Tests.Meta_Data;

public class ClassValidationMetadataTests
{

    [Fact]
    public void HasRequiredValidator_ShouldReturnTrue_WhenNotNullValidatorExists()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new("Name", new List<string> { "NotNullValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasRequiredValidator("Name");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasRequiredValidator_ShouldReturnTrue_WhenNotEmptyValidatorExists()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "NotEmptyValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasRequiredValidator("Name");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasRequiredValidator_ShouldReturnFalse_WhenNoRequiredValidatorExists()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "SomeOtherValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasRequiredValidator("Name");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasRequiredValidator_ShouldReturnFalse_WhenPropertyDoesNotExist()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "NotNullValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasRequiredValidator("NonExistentProperty");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasMaxLengthsValidator_ShouldReturnTrue_WhenMaximumLengthValidatorExists()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "MaximumLengthValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasMaxLengthsValidator("Name");

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void HasMaxLengthsValidator_ShouldReturnFalse_WhenNoMaximumLengthValidatorExists()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "SomeOtherValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasMaxLengthsValidator("Name");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void HasMaxLengthsValidator_ShouldReturnFalse_WhenPropertyDoesNotExist()
    {
        // Arrange
        var propertyValidators = new List<PropertyValidationMetadata>
        {
            new PropertyValidationMetadata("Name", new List<string> { "MaximumLengthValidator" })
        };
        var classValidationMetadata = new ClassValidationMetadata(propertyValidators);

        // Act
        var result = classValidationMetadata.HasMaxLengthsValidator("NonExistentProperty");

        // Assert
        result.ShouldBeFalse();
    }
}
