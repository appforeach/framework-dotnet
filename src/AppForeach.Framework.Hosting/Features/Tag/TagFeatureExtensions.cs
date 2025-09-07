using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AppForeach.Framework.Hosting.Features.Tag;

public static class TagFeatureExtensions
{
    public static void AddApplicationFeatureTag(this IServiceCollection services, string tag)
    {
        services.AddSingleton(new TagFeatureOption { Tag  = tag });
    }

    public static IReadOnlySet<string> GetApplicationFeatureTags(this IEnumerable<IApplicationFeatureOption> options)
    {
        var tags = options.OfType<TagFeatureOption>().Select(o =>  o.Tag).Distinct(StringComparer.OrdinalIgnoreCase);
        return ImmutableHashSet.Create(StringComparer.OrdinalIgnoreCase, tags.ToArray());
    }
}
