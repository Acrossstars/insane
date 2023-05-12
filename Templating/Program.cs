﻿using Microsoft.Extensions.Configuration;
using Models;
using Models.RequestHandler;
using Newtonsoft.Json;
using Scriban;
using Templating;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();


var requestHandlerDir = $"{Directory.GetCurrentDirectory()}\\RequestHandler";

var configFilePath = $"{requestHandlerDir}\\RequestHandlerConfig.json";
var textTemplatePath = $"{requestHandlerDir}\\RequestHandlerTextTemplate.txt";


var outputFilePath = "";

var builder = new ObjectBuilderMetadata()
{
    ObjectDataFilePath = configFilePath,
    TextTemplateFilePath = textTemplatePath,
    OutputFilePath = outputFilePath
};

var textTemplate = File.ReadAllText(builder.TextTemplateFilePath);

var tpl = Template.Parse(textTemplate);

var stringMetadata = File.ReadAllText(builder.ObjectDataFilePath);

//var scr = ScriptObject
var model = JsonConvert.DeserializeObject<RequestHandlerMetadata>(stringMetadata);

try
{
    //var model = new Dictionary<string, object>();

    //foreach (PropertyInfo prop in requestHandlerMetadata!.GetType().GetProperties())
    //{
    //    Console.WriteLine($"{prop.Name}: {prop.GetValue(requestHandlerMetadata, null)}");

    //    model.Add(prop.Name, prop.GetValue(requestHandlerMetadata, null));
    //}

    //only with new { model }
    var res = tpl.Render(model);

    //model.TryGetValue("ClassName", out var name);
    var name = model.ClassName;

    var file = /*builder.OutputFilePath + */$"{name}.cs";

    File.WriteAllText(file, res);

    Console.WriteLine(res);

    var fileLoader = new FileLoader();
    fileLoader.AddFileToProject($"{configuration["SolutionRootPath"]}\\src\\ConsoleApp\\SomeFolder", "SampleFile.cs", res);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

record User(string Namespace, string ClassName);