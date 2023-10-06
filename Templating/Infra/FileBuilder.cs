using Core;
using Core.Formatiing;
using Scriban;
using System.Runtime.Serialization;

namespace Templating.Infra;

public class FileBuilder
{
    public FileBuilder()
    {
    }

    public void Build(ObjectBuilderContext builderMetadata)
    {
        var textTemplate = File.ReadAllText(builderMetadata.TextTemplateFilePath);

        var tpl = Template.Parse(textTemplate);

        var model = builderMetadata.Model;

        try
        {
            var res = tpl.Render(model);
            var fileName = $"{builderMetadata.FileName}.cs";
            var fileDirectory = builderMetadata.OutputFilePath;

            var formattedCode = CodeFormatter.FormatCodeWithRoslyn(res);

            var fileLoader = new FileLoader();
            fileLoader.AddFileToProject(fileDirectory, fileName, formattedCode);

            Console.WriteLine(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}