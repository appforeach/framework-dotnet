using AppForeach.Framework.DataType;
using AppForeach.Framework.EntityFrameworkCore.DataType;
using Microsoft.EntityFrameworkCore;
using Shouldly;


namespace AppForeach.Framework.Tests;

public class EntityTypeBuilderExtensionsTests
{
    [Fact]
    public void should_apply_entity_configuration_from_model_specification()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new TestDbContext(options);
        var modelBuilder = new ModelBuilder();
        var entityBuilder = modelBuilder.Entity<Product>();

        //tweak this
        // Act
        entityBuilder.FromEntitySpecification();

        // Assert
        var entityType = modelBuilder.Model.FindEntityType(typeof(Product));
        var nameProperty = entityType.FindProperty(nameof(Product.Name));

        nameProperty.IsNullable.ShouldBe(false);
        nameProperty.GetMaxLength().ShouldBe(50);
    }

    private class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tweak this
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }
    }

    private class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private class ProductEntitySpecification : BaseEntitySpecification<Product>
    {
        public ProductEntitySpecification()
        {
            Field(e => e.Name).IsRequired().MaxLength(50);
        }
    }
}