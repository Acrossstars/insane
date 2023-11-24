namespace Templating.Configurations.Models;

public class ProjectConfig
{
    public string SolutionRootPath { get; set; } = string.Empty;
    public NamespacesConfig Namespaces { get; set; } = new();
    public OutputRelativePathsConfig OutputRelativePaths { get; set; } = new();
}

public class NamespacesConfig
{
    public string UseCasesBase { get; set; } = string.Empty;
}

public class OutputRelativePathsConfig
{
    public string UseCases { get; set; } = string.Empty;
}
