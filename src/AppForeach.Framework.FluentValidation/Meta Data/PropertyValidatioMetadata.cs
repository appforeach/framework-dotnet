using System.Diagnostics.CodeAnalysis;

namespace AppForeach.Framework.FluentValidation.Meta_Data;
internal class PropertyValidatioMetadata
{
    internal PropertyValidatioMetadata() { }
    [SetsRequiredMembers]
    internal PropertyValidatioMetadata(string name, IEnumerable<string> validators)
    {
        Name = name;
        Validators = validators;
    }
    internal required string Name { get; init; }
    internal required IEnumerable<string> Validators { get; init; }
}
