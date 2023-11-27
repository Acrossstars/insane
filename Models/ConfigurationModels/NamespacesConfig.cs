namespace Core.ConfigurationsModels;

public class NamespacesConfig
{
    public string EntitiesBase { get; set; } = string.Empty;
    public string UseCasesBase { get; set; } = string.Empty;
    public string Events { get; set; } = string.Empty;
    public string EventHandlers { get; set; } = string.Empty;
    public string Repositories { get; set; } = string.Empty;
    public string RepositoryInterfaces { get; set; } = string.Empty;
}
