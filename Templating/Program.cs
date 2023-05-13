using Microsoft.Extensions.Configuration;
using Models;
using Templating.Infra;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var filesToGenerate = new List<string>() { "CommandRequest", "CommandRequestHandler", "QueryRequest", "QueryRequestHandler", };


var builders = new List<ObjectBuilderMetadata>();

foreach (var item in filesToGenerate)
{
    var requestHandlerDir = $"{Directory.GetCurrentDirectory()}\\{item}";

    var configFilePath = $"{requestHandlerDir}\\{item}Config.json";
    var textTemplatePath = $"{requestHandlerDir}\\{item}TextTemplate.txt";

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