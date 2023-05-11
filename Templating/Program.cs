using Models;
using Models.RequestHandler;
using Newtonsoft.Json;
using Scriban;
using System.Reflection;

var configFilePath = "D:\\templates\\Templating\\Models\\\\RequestHandler\\RequestHandlerConfig.json";

var fileName = "D:\\templates\\Templating\\Templating\\templates\\RequestHandlerTemplate.tpl";

var textTemplatePath = "D:\\templates\\Templating\\Models\\\\RequestHandler\\RequestHandlerTextTemplate.txt";


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

var requestHandlerMetadata = JsonConvert.DeserializeObject<RequestHandlerMetadata>(stringMetadata);

try
{
    var model = new Dictionary<string, object>();

    foreach (PropertyInfo prop in requestHandlerMetadata.GetType().GetProperties())
    {
        Console.WriteLine($"{prop.Name}: {prop.GetValue(requestHandlerMetadata, null)}");

        model.Add(prop.Name, prop.GetValue(requestHandlerMetadata, null));
    }

    //only with new { model }
    var res = tpl.Render(new { model });

    model.TryGetValue("ClassName", out var name);

    var file = /*builder.OutputFilePath + */$"{name}.cs";

    File.WriteAllText(file, res);

    Console.WriteLine(res);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

record User(string Namespace, string ClassName);