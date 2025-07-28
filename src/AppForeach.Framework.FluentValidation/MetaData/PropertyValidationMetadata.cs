using System.Diagnostics.CodeAnalysis;

namespace AppForeach.Framework.FluentValidation.MetaData;
internal class PropertyValidationMetadata
{
    internal PropertyValidationMetadata() { }
    [SetsRequiredMembers]
    internal PropertyValidationMetadata(string name, IEnumerable<string> validators)
    {
        Name = name;
        Validators = validators;
    }
    internal required string Name { get; init; }
    internal required IEnumerable<string> Validators { get; init; }
}
