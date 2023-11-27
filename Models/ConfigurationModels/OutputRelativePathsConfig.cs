namespace Core.ConfigurationsModels;

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
