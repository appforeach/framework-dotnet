using AppForeach.Framework.DataType;
using Shouldly;
using System.Linq.Expressions;

namespace AppForeach.Framework.Tests
{
    public class BaseEntitySpecificationTests
    {
        [Fact]
        public void should_return_the_same_instance_of_facet_when_spec_referenced_multiple_times_via_static_seletor()
        {
            // Arrange
            var specification = new BaseEntitySpecification<string>();

            Expression<Func<string, int>> selector = x => x.Length;
            specification.Field(selector).Configuration.Set(new SomeFacet());

            var firstReference = specification.Field(selector).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();
            var secondReference = specification.Field(selector).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();
            var thirdReference = specification.Field(selector).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();

            firstReference.ShouldBe(secondReference);
            secondReference.ShouldBe(thirdReference);
        }

        [Fact]
        public void should_return_the_same_instance_of_facet_when_spec_referenced_multiple_times_via_lambda_selector()
        {
            // Arrange
            var specification = new BaseEntitySpecification<string>();
            specification.Field(x => x.Length).Configuration.Set(new SomeFacet());

            var firstReference = specification.Field(x => x.Length).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();
            var secondReference = specification.Field(x => x.Length).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();
            var thirdReference = specification.Field(x => x.Length).Configuration.TryGet<SomeFacet>().ShouldNotBeNull();

            firstReference.ShouldBe(secondReference);
            secondReference.ShouldBe(thirdReference);
        }

        private class SomeFacet
        {
        }
    }
}
