using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework.CodeGeneration.VisualStudio
{
    internal static class CommandTemplates
    {
        public const string CommandTemplate =
@"
using System.Threading.Tasks;

namespace {namespace}
{
    public class {logicalName}Command
    {
        private readonly I{logicalName}InputMapping inputMapping;
        private readonly I{logicalName}OutputMapping outputMapping;

        public {logicalName}Command(I{logicalName}InputMapping inputMapping,
            I{logicalName}OutputMapping outputMapping)
        {
            this.inputMapping = inputMapping;
            this.outputMapping = outputMapping;
        }

        public async Task<{logicalName}Output> Execute({logicalName}Input input)
        {
            var smth = inputMapping.MapFrom(input);

            return outputMapping.MapFrom(null);
        }
    }
}
";

        public const string CommandInputTemplate =
@"
namespace {namespace}
{
    public class {logicalName}Input
    {
    }
}
";

        public const string CommandInputMappingTemplate =
@"
namespace {namespace}
{
    public interface I{logicalName}InputMapping
    {
        object MapFrom({logicalName}Input input);
    }

    public class {logicalName}InputMapping : I{logicalName}InputMapping
    {
        public object MapFrom({logicalName}Input input)
        {
            return new object();
        }
    }
}
";

        public const string CommandOutputTemplate =
@"
namespace {namespace}
{
    public class {logicalName}Output
    {
    }
}
";

        public const string CommandOutputMappingTemplate =
@"
namespace {namespace}
{
    public interface I{logicalName}OutputMapping
    {
        {logicalName}Output MapFrom(object entity);
    }

    public class {logicalName}OutputMapping : I{logicalName}OutputMapping
    {
        public {logicalName}Output MapFrom(object entity)
        {
            return new {logicalName}Output();
        }
    }
}
";

        public const string CommandValidationTemplate =
@"
namespace {namespace}
{
    public interface I{logicalName}Validation
    {
        void Validate(I{logicalName});
    }

    public class {logicalName}Validation //: BaseValidation
    {
        public void Validate(I{logicalName})
        {
        }
    }
}
";
    }
}
