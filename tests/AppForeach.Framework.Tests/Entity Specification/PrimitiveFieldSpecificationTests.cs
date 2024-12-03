using AppForeach.Framework.DataType;
using AppForeach.Framework.DataType.Facets;
using Shouldly;

namespace AppForeach.Framework.Tests
{
    public class PrimitiveFieldSpecificationTests
    {
        readonly PrimitiveFieldSpecification<int> _specification = new PrimitiveFieldSpecification<int>();

        [Fact]
        public void shold_throw_when_Is_called()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _specification.Is<IDataType>());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void should_set_IsRequired(bool required)
        {
            _specification.IsRequired(required);
            _specification.Configuration.TryGet<FieldRequiredFacet>().Required.ShouldBe(required);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void should_set_IsOptional(bool optional)
        {
            _specification.IsOptional(optional);
            _specification.Configuration.TryGet<FieldRequiredFacet>().Required.ShouldBe(!optional);
        }
    }
}
