using Core.ConfigurationsModels;

namespace Core.Generation;

public class NamespaceService
{
    private readonly ProjectConfig _projectConfig;

    public NamespaceService(ProjectConfig projectConfig)
    {
        _projectConfig = projectConfig;
    }

    public string CreateEntityNamespace(string manyEntities)
        => $"{_projectConfig.Namespaces.EntitiesBase}.{manyEntities}";

    public string CreateUseCaseNamespace(string manyEntities, string useCaseName)
        => $"{_projectConfig.Namespaces.UseCasesBase}.{manyEntities}.{useCaseName}";

    public string CreateEventsNamespace(string manyEntities)
        => $"{_projectConfig.Namespaces.Events}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateEventHandlersNamespace(string manyEntities)
        => $"{_projectConfig.Namespaces.EventHandlers}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateRepsitoriesNamespace(string manyEntities)
        => $"{_projectConfig.Namespaces.Repositories}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateRepositoryInterfacesNamespace(string manyEntities)
        => $"{_projectConfig.Namespaces.RepositoryInterfaces}"
            .Replace("${MANY_ENTITIES}", manyEntities);
}
