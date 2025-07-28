using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.Tests.Entity_Specification.Data;
using Shouldly;

namespace AppForeach.Framework.Tests.Entity_Specification
{
    public class UserEntitySpecificationTests
    {
        [Fact]
        public void should_take_facet_from_base_primitive_type_when_not_specified_on_field()
        {
            var specification = new UserEntitySpecification();

            var fieldSpec = specification.FieldSpecifications;
            fieldSpec.TryGetValue(nameof(UserEntity.FirstName),
                out var firstNameSpecfication).ShouldBeTrue();

            var maxLengthFacet = firstNameSpecfication.Configuration.TryGet<FieldMaxLengthFacet>()
                .ShouldNotBeNull();

            maxLengthFacet.MaxLength.ShouldBe(50);
        }

        [Fact]
        public void should_take_facet_from_field_when_specified()
        {
            var specification = new UserEntitySpecification();

            var fieldSpec = specification.FieldSpecifications;
            fieldSpec.TryGetValue(nameof(UserEntity.LastName),
                out var firstNameSpecfication).ShouldBeTrue();

            var maxLengthFacet = firstNameSpecfication.Configuration.TryGet<FieldMaxLengthFacet>()
                .ShouldNotBeNull();

            maxLengthFacet.MaxLength.ShouldBe(20);
        }

        [Fact]
        public void should_take_base_type_facet_for_nullable_type()
        {
            var specification = new UserEntitySpecification();

            var fieldSpec = specification.FieldSpecifications;
            fieldSpec.TryGetValue(nameof(UserEntity.ScoreAdditional),
                out var scoreSpecification).ShouldBeTrue();

            var precisionFacet = scoreSpecification.Configuration.TryGet<FieldPrecisionFacet>()
                .ShouldNotBeNull();

            precisionFacet.Precision.ShouldBe(4);
            precisionFacet.Scale.ShouldBe(2);
        }
    }
}
