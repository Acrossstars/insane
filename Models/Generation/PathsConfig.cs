using Core.Generation.Interface;

namespace Core.Generation;
public class PathsConfig : IEventsPathInterface, IRepositoriesPathInterface
{
    public PathsConfig()
    {
        SolutionRootPath = "C:\\rep\\draft\\subject";
        ApiRootPath = "\\Services\\Gleb.API";
        DtoPath = "\\BuildingBlocks\\Contracts\\Dtos";
        DomainLayerPath = "\\Services\\BoundedContexts";
        ApplicationLayerPath = "\\Services\\ApplicationOld";
    }

    public PathsConfig(string? solutionRootPath, string? apiRootPath, string? dtoPath, string? domainLayerPath, string? applicationLayerPath)
    {
        SolutionRootPath = solutionRootPath;
        ApiRootPath = apiRootPath;
        DtoPath = dtoPath;
        DomainLayerPath = domainLayerPath;
        ApplicationLayerPath = applicationLayerPath;
    }

    public string? SolutionRootPath { get; set; }
    public string? ApiRootPath { get; set; }
    public string? DtoPath { get; set; }
    public string? DomainLayerPath { get; set; }
    public string? ApplicationLayerPath { get; set; }
    public string? EventsOutputFilePath { get; set; }
    public string? EventHandlersOutputFilePath { get; set; }
    public string? RepositoryOutputFilePath { get; set; }
    public string? RepositoryInterfaceOutputFilePath { get; set; }
}