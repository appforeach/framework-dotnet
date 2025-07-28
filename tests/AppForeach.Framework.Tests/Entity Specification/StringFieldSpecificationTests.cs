using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using Shouldly;

namespace AppForeach.Framework.Tests.Entity_Specification;


public class StringFieldSpecificationTests
{
    readonly PrimitiveFieldSpecification<string> _specification = new PrimitiveFieldSpecification<string>(new FacetBag());

    [Fact]
    public void shold_throw_when_Is_called()
    {
        // Act & Assert
        Assert.Throws<NotImplementedException>(() => _specification.Is<IDataType>());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(250)]
    public void should_set_IsRequired(int maxLength)
    {
        _specification.HasMaxLength(maxLength);
        _specification.Configuration.TryGet<FieldMaxLengthFacet>().MaxLength.ShouldBe(maxLength);
    }
}