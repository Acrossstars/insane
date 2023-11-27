using Core.ConfigurationsModels;

namespace Core.Generation;

public class PathService
{
    private readonly ProjectConfig _projectConfig;

    public PathService(ProjectConfig projectConfig)
    {
        _projectConfig = projectConfig;
    }

    public string CreateEntityPath(string manyEntities)
       => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.Entities}\\{manyEntities}"
        .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateUseCasePath(string manyEntities, string useCaseName)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.UseCases}\\{manyEntities}\\{useCaseName}";

    public string CreateDtosPath(string manyEntities)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.Dtos}\\{manyEntities}";

    public string CreateEventsPath(string manyEntities)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.Events}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateEventHandlersPath(string manyEntities)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.EventHandlers}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateRepositoryPath(string manyEntities)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.Repositories}"
            .Replace("${MANY_ENTITIES}", manyEntities);

    public string CreateRepositoryInterfasePath(string manyEntities)
        => $"{_projectConfig.SolutionRootPath}\\{_projectConfig.OutputRelativePaths.RepositoryInterfaces}"
            .Replace("${MANY_ENTITIES}", manyEntities);
}