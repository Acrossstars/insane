using Scriban.Parsing;
using Scriban.Runtime;
using Scriban;

namespace Templating.Infra;

public class FileTemplateLoader : ITemplateLoader
{
    private readonly string _templateDirectory;
    private readonly string _extension;

    public FileTemplateLoader(string templateDirectory, string extension = ".scriban")
    {
        _templateDirectory = templateDirectory;
        _extension = extension;
    }

    public string GetPath(TemplateContext context, SourceSpan callerSpan, string templateName)
    {
        return Path.Combine(_templateDirectory, templateName + _extension);
    }

    public string Load(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        return File.ReadAllText(templatePath);
    }

    public string Load(TemplateContext context, SourceSpan callerSpan, string templateName, out string templatePath)
    {
        templatePath = GetPath(context, callerSpan, templateName);
        return Load(context, callerSpan, templatePath);
    }

    public ValueTask<string> LoadAsync(TemplateContext context, SourceSpan callerSpan, string templatePath)
    {
        throw new NotImplementedException();
    }
}
