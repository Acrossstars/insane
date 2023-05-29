using Core;
using Microsoft.Extensions.Configuration;
using Scriban;

namespace Templating.Infra;

public class FileBuilder
{
    private readonly IConfigurationRoot _configuration;

    public FileBuilder()
    {
    }

    public FileBuilder(IConfigurationRoot configuration)
    {
        _configuration = configuration;
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

            var fileLoader = new FileLoader();
            fileLoader.AddFileToProject(fileDirectory, fileName, res);

            Console.WriteLine(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}