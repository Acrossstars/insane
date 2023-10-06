namespace Core.Generation;
public class PathConfig
{
    public PathConfig()
    {
        SolutionRootPath = "C:\\rep\\draft\\subject";
        ApiRootPath = "\\Services\\Gleb.API";
        DtoPath = "\\BuildingBlocks\\Contracts\\Dtos";
        DomainLayerPath = "\\Services\\BoundedContexts";
        ApplicationLayerPath = "\\Services\\ApplicationOld";
    }

    public PathConfig(string? solutionRootPath, string? apiRootPath, string? dtoPath, string? domainLayerPath, string? applicationLayerPath)
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
}