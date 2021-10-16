using System.Collections.Generic;

namespace AppForeach.Framework.CodeGeneration.VisualStudio
{
    internal static class CommandGenerator
    {
        public static GeneratedFile[] GenerateFiles(string typeName, string nameSpace)
        {
            string logicalName = typeName.Substring(0, typeName.LastIndexOf("Command"));

            List<GeneratedFile> files = new List<GeneratedFile>();

            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandTemplate });

            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandInputTemplate, FileName = "{logicalName}Input.cs" });
            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandInputMappingTemplate, FileName = "{logicalName}InputMapping.cs" });
            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandOutputTemplate, FileName = "{logicalName}Output.cs" });
            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandOutputMappingTemplate, FileName = "{logicalName}OutputMapping.cs" });
            files.Add(new GeneratedFile { FileText = CommandTemplates.CommandValidationTemplate, FileName = "{logicalName}Validation.cs" });

            foreach (var result in files)
            {
                result.FileText = GetFileText(result.FileText, logicalName, nameSpace);
                
                if(!string.IsNullOrEmpty(result.FileName))
                {
                    result.FileName = result.FileName.Replace("{logicalName}", logicalName);
                }
            }           

            return files.ToArray();
        }

        private static string GetFileText(string template, string logicalName, string nameSpace)
        {
            return template.Replace("{logicalName}", logicalName).Replace("{namespace}", nameSpace);
        }
    }
}
