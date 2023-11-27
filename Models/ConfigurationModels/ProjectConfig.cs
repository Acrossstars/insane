namespace Core.ConfigurationsModels;

public class ProjectConfig
{
    public string SolutionRootPath { get; set; } = string.Empty;
    public NamespacesConfig Namespaces { get; set; } = new();
    public OutputRelativePathsConfig OutputRelativePaths { get; set; } = new();
}