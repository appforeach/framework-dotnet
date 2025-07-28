using AppForeach.Framework.DataType;
using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.FluentValidation.MetaData;
using FluentValidation;
using FluentValidation.Results;
using Shouldly;

namespace AppForeach.Framework.FluentValidation.Tests
{
    public class ValidationApplicationServiceTests
    {
        private Func<IEnumerable<PropertyValidationMetadata>>? getExistingValidation;

        private Action<FacetBag>? setupFacets;

        [Fact]
        public void ApplyToValidator_Should_Set_NotNullValidator()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
            };

            var command = new TestCommand();

            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }



        [Fact]
        public void ApplyToValidator_Should_Not_Set_NotNullValidator_WhenExisting()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = ["NotNullValidator"]
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
            };

            var command = new TestCommand();


            // Act
            var result = getValidationResult(command, nameof (TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Fail_Null_When_Required_And_EmptyAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = true });
            };

            var command = new TestCommand { StringField = null };

            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void ApplyToValidator_Should_Pass_Empty_When_Required_And_EmptyAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = true });
            };

            var command = new TestCommand { StringField = string.Empty };

            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Fail_Null_When_Required_And_EmptyNotAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = false });
            };

            var command = new TestCommand { StringField = null };

            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void ApplyToValidator_Should_Fail_Empty_When_Required_And_EmptyNotAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldRequiredFacet { Required = true });
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = false });
            };

            var command = new TestCommand { StringField = string.Empty };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void ApplyToValidator_Should_Pass_Null_When_NotRequired_And_EmptyAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = true });
            };

            var command = new TestCommand { StringField = null };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Pass_Empty_When_NotRequired_And_EmptyAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = true });
            };

            var command = new TestCommand { StringField = string.Empty };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Pass_Null_When_NotRequired_And_EmptyNotAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = false });
            };

            var command = new TestCommand { StringField = null };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Fail_Empty_When_NotRequired_And_EmptyNotAllowed()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldIsEmptyAllowedFacet { IsEmptyAllowed = false });
            };

            var command = new TestCommand { StringField = string.Empty };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }


        [Fact]
        public void ApplyToValidator_Should_Set_MaximumLengthValidator()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldMaxLengthFacet { MaxLength = 3 });
            };

            var command = new TestCommand { StringField = new string('a', 4) };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void ApplyToValidator_Should_Not_Set_MaximumLength_WhenExisting()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.StringField),
                    Validators = ["MaximumLengthValidator"]
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldMaxLengthFacet { MaxLength = 3 });
            };

            var command = new TestCommand { StringField = new string('a', 4) };


            // Act
            var result = getValidationResult(command, nameof(TestCommand.StringField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ApplyToValidator_Should_Set_Precision()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.DecimalField),
                    Validators = []
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldPrecisionFacet { Precision = 4, Scale = 2 });
            };

            var command = new TestCommand { DecimalField = 123.45m };

            // Act
            var result = getValidationResult(command, nameof(TestCommand.DecimalField));

            // Assert
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void ApplyToValidator_Should_Not_Set_Precision_WhenExisting()
        {
            // Arrange
            getExistingValidation = () =>
            [
                new PropertyValidationMetadata
                {
                    Name = nameof(TestCommand.DecimalField),
                    Validators = ["ScalePrecisionValidator"]
                }
            ];

            setupFacets = facets =>
            {
                facets.Set(new FieldPrecisionFacet{ Precision = 4, Scale = 2 });
            };

            var command = new TestCommand { DecimalField = 123.45m };

            // Act
            var result = getValidationResult(command, nameof(TestCommand.DecimalField));

            // Assert
            result.IsValid.ShouldBeTrue();
        }

        private ValidationResult getValidationResult(TestCommand command, string propertyName)
        {
            var existingValidation = new ClassValidationMetadata(
                getExistingValidation?.Invoke() ?? Enumerable.Empty<PropertyValidationMetadata>()
            );

            FacetBag facets = new FacetBag();
            setupFacets?.Invoke(facets);
            var specification = new PrimitiveFieldSpecification(facets);

            var applicationService = new ValidationApplicationService(existingValidation);

            var validator = new TestCommandValidator();
            applicationService.ApplyToValidator(specification, propertyName, validator);
            return validator.Validate(command);
        }

        public class TestCommand
        {
            public string? StringField { get; set; }

            public decimal? DecimalField { get; set; }
        }

        public class TestCommandValidator : AbstractValidator<TestCommand>
        {
            public TestCommandValidator()
            {
            }
        }
    }
}
