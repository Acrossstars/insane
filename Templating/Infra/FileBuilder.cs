using Core;
using Core.Formatiing;
using Scriban;
using Scriban.Runtime;
using System.Reflection;

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

        var scriptObject = InitializeScriptObjectWithProperties(model);
        var context = InitializeTemplateContext(scriptObject, builderMetadata.TextTemplateFilePath);

        try
        {
            var res = tpl.Render(context);
            // User.cs
            var fileName = $"{builderMetadata.FileName}.cs";
            // "C:\\Users\\human\\source\\repos\\AppFox\\scumdoffback\\src\\Services\\Scumdoff.AdminPanel\\Domain\\Users"
            var fileDirectory = builderMetadata.OutputFilePath;

            var formattedCode = CodeFormatter.FormatCodeWithRoslyn(res);

            var fileLoader = new FileLoader();
            fileLoader.AddFileToProject(fileDirectory, fileName, formattedCode);

            Console.WriteLine(res);

            var outputFileLoader = new FileLoader($"Configurations/Projects/Scumdoff.AdminPanel.json");
            outputFileLoader.AddOutputFilesToFolder(fileName, fileDirectory);

            //var forArduinoFileDirectory = 
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private TemplateContext InitializeTemplateContext(ScriptObject scriptObject, string path)
    {
        var context = new TemplateContext();
        context.PushGlobal(scriptObject);

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path), "Path cannot be null or empty.");
        }

        string templateDir = Path.GetDirectoryName(path)
            ?? throw new ArgumentException("Invalid template directory path.", nameof(path));

        context.TemplateLoader = new FileTemplateLoader(templateDir);

        return context;
    }

    private ScriptObject InitializeScriptObjectWithProperties(object obj)
    {
        var scriptObject = new ScriptObject();

        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            scriptObject.Add(property.Name, property.GetValue(obj));
        }

        return scriptObject;
    }
}