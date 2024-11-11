using AppForeach.Framework.DataType;
using Shouldly;

namespace AppForeach.Framework.Tests
{
    public class PrimitiveTypeSpecificationTests
    {
        readonly PrimitiveTypeSpecification<int> _specification = new PrimitiveTypeSpecification<int>();

        [Fact]
        public void shold_throw_when_Is_called()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _specification.Is<IDataType>());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void should_set_IsRequired(bool value)
        {
            _specification.IsRequired(value);
            _specification.Config.IsRequired.ShouldBe(value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void should_set_IsOptional(bool value)
        {
            _specification.IsOptional(value);
            _specification.Config.IsOptional.ShouldBe(value);
        }
    }
}
