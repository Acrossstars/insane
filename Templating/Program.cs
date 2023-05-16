using Microsoft.Extensions.Configuration;
using Core;
using Templating.Infra;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var filesToGenerate = new List<string>() { "CommandRequest", "CommandRequestHandler", "QueryRequest", "QueryRequestHandler", };


var builders = new List<ObjectBuilderMetadata>();

foreach (var item in filesToGenerate)
{
    var metadataDir = $"{Directory.GetCurrentDirectory()}";
    //var metadataDir = $"D:\\templates\\Templating\\Core\\Configs";

    var configFilePath = $"{metadataDir}\\Configs\\{item}Config.json";
    var textTemplatePath = $"{metadataDir}\\TextTemplates\\{item}TextTemplate.txt";

    var outputFilePath = "";

    builders.Add(new ObjectBuilderMetadata()
    {
        ObjectDataFilePath = configFilePath,
        TextTemplateFilePath = textTemplatePath,
        OutputFilePath = outputFilePath
    });
}

foreach (var builderMetadata in builders)
{
    var builder = new FileBuilder(configuration);
    builder.Build(builderMetadata);
}