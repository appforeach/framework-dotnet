using AppForeach.Framework.Tests.Entity_Specification.Data;
using Shouldly;

namespace AppForeach.Framework.Tests
{
    public class InvoiceEntitySpecificationTests
    {
        [Fact]
        private void should_have_default_values()
        {
            var specification = new InvoiceEntitySpecification();
            var fieldSpec = specification.Field(x => x.CustomerNumber);
            fieldSpec.Config.IsRequired.ShouldBe(true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_customerNumber_required_configurations(bool required)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.CustomerNumber).IsRequired(required);

            var fieldSpec = specification.Field(x => x.CustomerNumber);

            fieldSpec.Config.IsRequired.ShouldBe(required);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_customerNumber_optional_configurations(bool optional)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.CustomerNumber).IsOptional(optional);

            var fieldSpec = specification.Field(x => x.CustomerNumber);

            fieldSpec.Config.IsOptional.ShouldBe(optional);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_id_required_configurations(bool required)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Id).IsRequired(required);

            var fieldSpec = specification.Field(x => x.Id);

            fieldSpec.Config.IsRequired.ShouldBe(required);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_id_optional_configurations(bool optional)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Id).IsOptional(optional);

            var fieldSpec = specification.Field(x => x.Id);

            fieldSpec.Config.IsOptional.ShouldBe(optional);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_number_required_configurations(bool required)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Number).IsRequired(required);

            var fieldSpec = specification.Field(x => x.Number);

            fieldSpec.Config.IsRequired.ShouldBe(required);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_number_optional_configurations(bool optional)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Number).IsOptional(optional);

            var fieldSpec = specification.Field(x => x.Number);

            fieldSpec.Config.IsOptional.ShouldBe(optional);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_amout_required_configurations(bool required)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Amount).IsRequired(required);

            var fieldSpec = specification.Field(x => x.Amount);

            fieldSpec.Config.IsRequired.ShouldBe(required);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        private void should_persist_amount_optional_configurations(bool optional)
        {
            var specification = new InvoiceEntitySpecification();
            specification.Field(x => x.Amount).IsOptional(optional);

            var fieldSpec = specification.Field(x => x.Amount);

            fieldSpec.Config.IsOptional.ShouldBe(optional);
        }
    }
}
