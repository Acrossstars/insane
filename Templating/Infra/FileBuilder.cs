using Models.RequestHandler;
using Models;
using Newtonsoft.Json;
using Scriban;
using Microsoft.Extensions.Configuration;

namespace Templating.Infra;

public class FileBuilder
{
    private IConfigurationRoot _configuration;

    public FileBuilder()
    {
    }

    public FileBuilder(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }

    public void Build(ObjectBuilderMetadata builderMetadata)
    {
        var textTemplate = File.ReadAllText(builderMetadata.TextTemplateFilePath);

        var tpl = Template.Parse(textTemplate);

        var stringMetadata = File.ReadAllText(builderMetadata.ObjectDataFilePath);

        var model = JsonConvert.DeserializeObject<CommandRequestHandlerMetadata>(stringMetadata);

        try
        {
            var res = tpl.Render(model);
            var name = model.ClassName;
            var fileName = /*builderMetadata.OutputFilePath + */$"{name}.cs";
            var fileDirectory = $"{_configuration["SolutionRootPath"]}\\Draft";

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