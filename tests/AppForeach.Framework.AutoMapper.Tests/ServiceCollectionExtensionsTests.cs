using AppForeach.Framework.AutoMapper.Metadata;
using AppForeach.Framework.Mapping;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace AppForeach.Framework.AutoMapper.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAutoMapper_ShouldRegisterAutoMapper()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act

        services.AddFrameworkModule<AutoMapperFrameworkModule>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensionsTests));


        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var mapper = serviceProvider.GetService<IMapper>();
        mapper.ShouldNotBeNull();
    }

    [Fact]
    public void AddAutoMapper_ShouldRegisterMappingMetadataProvider()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFrameworkModule<AutoMapperFrameworkModule>();
        services.AddAutoMapper(typeof(ServiceCollectionExtensionsTests));

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var metadataProvider = serviceProvider.GetService<IMappingMetadataProvider>();

        metadataProvider
            .ShouldNotBeNull()
            .ShouldBeOfType<MappingMetadataProvider>();
    }
}
