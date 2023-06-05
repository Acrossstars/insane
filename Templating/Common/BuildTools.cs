using Core;
using Core.Domain.Common;

namespace Templating.Features;

public class BuildTools
{
    public BuildTools()
    {

    }

    static string GetTextTemplatePath(object item, string metadataDir)
    {
        var metadata = (BaseMetadata)item;

        return metadata.Type switch
        {
            _ => $"{metadataDir}\\TextTemplates\\{metadata.Type}TextTemplate.txt",
        };
    }

    public static void AppendToBuild(string metadataDir, List<ObjectBuilderContext> builderContexts, string outputFilePath, object model, string fileName)
    {
        builderContexts.Add(new ObjectBuilderContext()
        {
            FileName = fileName,
            Model = model,
            TextTemplateFilePath = GetTextTemplatePath(model, metadataDir),
            OutputFilePath = outputFilePath
        });
    }

}
