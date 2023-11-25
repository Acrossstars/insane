namespace Core.ConfigurationsModels;

public class ProjectConfig
{
    public string SolutionRootPath { get; set; } = string.Empty;
    public NamespacesConfig Namespaces { get; set; } = new();
    public OutputRelativePathsConfig OutputRelativePaths { get; set; } = new();
}

public class NamespacesConfig
{
    public string EntitiesBase { get; set; } = string.Empty;
    public string UseCasesBase { get; set; } = string.Empty;
    public string Events { get; set; } = string.Empty;
    public string EventHandlers { get; set; } = string.Empty;
    public string Repositories { get; set; } = string.Empty;
    public string RepositoryInterfaces { get; set; } = string.Empty;
}

public class OutputRelativePathsConfig
{
    public string Entities { get; set; } = string.Empty;
    public string UseCases { get; set; } = string.Empty;
    public string Dtos { get; set; } = string.Empty;
    public string Events { get; set; } = string.Empty;
    public string EventHandlers { get; set; } = string.Empty;
    public string Repositories { get; set; } = string.Empty;
    public string RepositoryInterfaces { get; set; } = string.Empty;
}
