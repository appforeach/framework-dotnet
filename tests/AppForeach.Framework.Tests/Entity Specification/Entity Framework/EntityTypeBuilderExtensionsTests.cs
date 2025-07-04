using AppForeach.Framework.DataType;
using AppForeach.Framework.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        // Act
        context.TriggerOnModelCreating(modelBuilder);

        // Assert
        var entityType = modelBuilder.Model.FindEntityType(typeof(Product));
        var nameProperty = entityType?.FindProperty(nameof(Product.Name));

        nameProperty?.IsNullable.ShouldBe(false);
        nameProperty?.GetMaxLength().ShouldBe(50);
    }

    [Fact]
    public void should_apply_precision_entity_configuration_from_model_specification()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new TestDbContext(options);
        var modelBuilder = new ModelBuilder();

        // Act
        context.TriggerOnModelCreating(modelBuilder);

        // Assert
        var entityType = modelBuilder.Model.FindEntityType(typeof(Product));
        var priceProperty = entityType?.FindProperty(nameof(Product.Price));

        priceProperty?.GetPrecision().ShouldBe(4);
        priceProperty?.GetScale().ShouldBe(2);
    }


    [Fact]
    public void should_not_apply_entity_configuration_from_model_specification_when_entity_configuration_is_not_setup()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new TestDbContext(options);
        var modelBuilder = new ModelBuilder();

        // Act
        context.TriggerOnModelCreating(modelBuilder);

        // Assert
        var entityType = modelBuilder.Model.FindEntityType(typeof(Customer));
        entityType.ShouldBe(null);
    }

    private class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
        }

        public void TriggerOnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);
        }
    }

    private class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public decimal? Price { get; set; }
    }

    private class ProductEntitySpecification : BaseEntitySpecification<Product>
    {
        public ProductEntitySpecification()
        {
            Field(e => e.Name).IsRequired().HasMaxLength(50);
            Field(e => e.Price).HasPrecision(4, 2);
        }
    }

    private class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.InheritFromEntitySpecification();
        }
    }

    private class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private class CustomerSpecification : BaseEntitySpecification<Customer>
    {
        public CustomerSpecification()
        {
            Field(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}