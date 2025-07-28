using AppForeach.Framework.DataType;
using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DependencyInjection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public static class EntityTypeBuilderExtensions
    {
        public static void InheritFromEntitySpecification<T>(this EntityTypeBuilder<T> builder) where T : class
        {
            var assemblyToScan = typeof(T).Assembly;
            var types = assemblyToScan.GetTypes();

            var scanner = new DefaulEntitySpecificationScanner();

            var entitySpecificationTypeToFind = typeof(BaseEntitySpecification<>).MakeGenericType(typeof(T));

            var scannedDefinitions = scanner
                .ScanTypes(types)
                .Where(t => entitySpecificationTypeToFind.IsAssignableFrom(t.ComponentType));

            foreach (var scannedDefinition in scannedDefinitions)
            {
                var entitySpecification = (BaseEntitySpecification<T>)Activator.CreateInstance(scannedDefinition.ComponentType);

                //todo: make field name part of specification
                foreach (var fieldSpecification in entitySpecification.FieldSpecifications)
                {
                    var facets = fieldSpecification.Value.Configuration;

                    var propertyBuilder = builder.Property(fieldSpecification.Key);

                    var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                    if (requiredFacet is not null)
                    {
                        propertyBuilder.IsRequired(requiredFacet.Required);
                    }

                    var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                    if (maxLengthFacet is not null)
                    {
                        propertyBuilder.HasMaxLength(maxLengthFacet.MaxLength);
                    }

                    var precisionFacet = facets.TryGet<FieldPrecisionFacet>();
                    if (precisionFacet is not null)
                    {
                        propertyBuilder.HasPrecision(precisionFacet.Precision, precisionFacet.Scale);
                    }
                }
            }
        }
    }
}
