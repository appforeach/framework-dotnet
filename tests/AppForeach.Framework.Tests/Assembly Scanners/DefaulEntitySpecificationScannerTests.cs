using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.DataType;
using Shouldly;


namespace AppForeach.Framework.Tests.DependencyInjection
{
    public class DefaulEntitySpecificationScannerTests
    {
        private class TestEntitySpecification : BaseEntitySpecification<int> { }

        [Fact]
        public void should_find_specifications_when_they_exist()
        {
            // Arrange
            var scanner = new DefaulEntitySpecificationScanner();

            var types = new List<Type> { typeof(TestEntitySpecification) };

            // Act
            var result = scanner.ScanTypes(types);

            // Assert
            result.ShouldNotBeEmpty();
            var componentDefinition = result.ShouldHaveSingleItem();
            componentDefinition.ComponentType.ShouldBe(typeof(TestEntitySpecification));
            componentDefinition.ImplementationType.ShouldBe(typeof(TestEntitySpecification));
            componentDefinition.Lifetime.ShouldBe(ComponentLifetime.Transient);
        }

        [Fact]
        public void should_not_find_specifications_when_they_does_not_exist()
        {
            // Arrange
            var scanner = new DefaulEntitySpecificationScanner();
            var types = new List<Type> { typeof(string) };

            // Act
            var result = scanner.ScanTypes(types);

            // Assert
            result.ShouldBeEmpty();
        }
    }
}
