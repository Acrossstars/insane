using Core.Generation.Interface;
using Microsoft.Extensions.Configuration;

namespace Core.Generation;
public class PathNameSpacesService
{
    private readonly IConfiguration _configuration;
    private readonly GenerationDesign _generationDesign;

    public string EventHandlersOutputFilePath { get; set; }

    public PathNameSpacesService(
        IConfiguration configuration,
        GenerationDesign generationDesign
        )
    {
        _configuration = configuration;
        _generationDesign = generationDesign;
    }

    public string GetSolutionRootPath()
        => _configuration["SolutionRootPath"]!;

    public string GetApiRootPath()
        => _configuration["ApiRootPath"]!;

    public string GetDomainLayerPath()
        => _configuration["DomainLayerPath"]!;

    public string? GetDomainLayerNamespaceRoot()
        => _configuration["DomainLayerNamespaceRoot"];

    public string GetApplicationLayerPath()
        => _configuration["ApplicationLayerPath"]!;

    public string GetDtosPath()
        => _configuration["SolutionRootPath"] + _configuration["DtoPath"];

    public string GetInfrastructurePath()
        => _configuration["InfrastructureLayerPath"]!;

    public IEventsPathInterface GetEventsPathConfig(string manyEntities)
    {
        var eventsInternalPath = $"\\{manyEntities}\\{_generationDesign.EventsFolderName}";
        var eventHandlersInternalPath = $"\\{manyEntities}\\{_generationDesign.EventHandlersFolderName}";

        var pathConfig = new PathsConfig()
        {
            EventsOutputFilePath = $"{GetSolutionRootPath()}{GetDomainLayerPath()}{eventsInternalPath}",
            EventHandlersOutputFilePath = $"{GetSolutionRootPath()}{GetApplicationLayerPath()}{eventHandlersInternalPath}"
        };

        return pathConfig;
    }

    public (string eventsNamespace, string eventHandlersNamespace) GetEventsGeneratedNamespace(string manyEntities)
    {
        var eventsInternalPath = $"\\{manyEntities}\\{_generationDesign.EventsFolderName}";
        var eventHandlersInternalPath = $"\\{manyEntities}\\{_generationDesign.EventHandlersFolderName}";

        var eventsGeneratedNamespace = $"{GetDomainLayerNamespaceRoot()}{eventsInternalPath.Replace("\\", ".")}";
        var eventHandlersGeneratedNamespace = $"{GetApplicationLayerPath().Split("\\").Last()}{eventHandlersInternalPath.Replace("\\", ".")}";

        return (eventsGeneratedNamespace, eventHandlersGeneratedNamespace);
    }

    public IRepositoriesPathInterface GetRepositoriesPathConfig(string manyEntities)
    {
        var repositoryInternalPath = $"\\{_generationDesign.RepositoriesDataFolderName}\\{_generationDesign.RepositoriesFolderName}\\{manyEntities}";
        var repositoryInterfaceInternalPath = $"\\{manyEntities}\\{_generationDesign.RepositoriesDataFolderName}\\{_generationDesign.RepositoriesFolderName}";

        var pathConfig = new PathsConfig()
        {
            RepositoryOutputFilePath = $"{GetSolutionRootPath()}{GetInfrastructurePath()}{repositoryInternalPath}",
            RepositoryInterfaceOutputFilePath = $"{GetSolutionRootPath()}{GetDomainLayerPath()}{repositoryInterfaceInternalPath}"
        };

        return pathConfig;
    }

}