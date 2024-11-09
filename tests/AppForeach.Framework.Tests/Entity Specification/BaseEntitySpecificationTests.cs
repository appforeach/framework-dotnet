using AppForeach.Framework.DataType;
using Shouldly;
using System.Linq.Expressions;

namespace AppForeach.Framework.Tests
{
    public class BaseEntitySpecificationTests
    {
        [Fact]
        public void should_return_the_same_instance_of_field_when_spec_referenced_multiple_times_via_static_seletor()
        {
            // Arrange
            var specification = new BaseEntitySpecification<string>();

            Expression<Func<string, string>> selector = x => x.Length.ToString();

            var firstReference = specification.Field(selector).ShouldNotBeNull();
            var secondReference = specification.Field(selector).ShouldNotBeNull();
            var thirdReference = specification.Field(selector).ShouldNotBeNull();

            firstReference.ShouldBe(secondReference);
            secondReference.ShouldBe(thirdReference);
        }

        [Fact]
        public void should_return_the_same_instance_of_field_when_spec_referenced_multiple_times_via_lambda_selector()
        {
            // Arrange
            var specification = new BaseEntitySpecification<string>();

            var firstReference = specification.Field(x => x.Length);
            var secondReference = specification.Field(x => x.Length);
            var thirdReference = specification.Field(x => x.Length);

            firstReference.ShouldBe(secondReference);
            secondReference.ShouldBe(thirdReference);
        }


        [Fact]
        public void should_return_field_specification()
        {
            // Arrange
            var specification = new BaseEntitySpecification<int>();
            Expression<Func<int, int>> selector = x => x * 2;
            var expectedField = specification.Field(selector);

            expectedField.ShouldNotBeNull();
        }
    }
}
