using AppForeach.Framework.Automapper.Metadata;
using AppForeach.Framework.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace AppForeach.Framework.Automapper.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAutoMapper_ShouldRegisterAutoMapper()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAutoMapper<ServiceCollectionExtensionsTests>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetService<AutoMapper.IMapper>();
        mapper.ShouldNotBeNull();
    }

    [Fact]
    public void AddAutoMapper_ShouldRegisterMappingMetadataProvider()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAutoMapper<ServiceCollectionExtensionsTests>();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var metadataProvider = serviceProvider.GetService<IMappingMetadataProvider>();

        metadataProvider
            .ShouldNotBeNull()
            .ShouldBeOfType<MappingMetadataProvider>();
    }
}
