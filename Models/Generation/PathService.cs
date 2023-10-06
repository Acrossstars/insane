using Microsoft.Extensions.Configuration;

namespace Core.Generation;
public class PathService
{
    private readonly IConfiguration _configuration;
    private readonly GenerationDesign _generationDesign;

    public PathService(
        IConfiguration configuration,
        GenerationDesign generationDesign
        )
    {
        _configuration = configuration;
        _generationDesign = generationDesign;



    }

    public string GetSolutionRootPath()
        => _configuration["SolutionRootPath"]!;

    public string GetDomainLayerPath()
        => _configuration["DomainLayerPath"]!;

    public string? GetDomainLayerNamespaceRoot()
        => _configuration["DomainLayerNamespaceRoot"];

    public string GetApplicationLayerPath()
        => _configuration["ApplicationLayerPath"]!;

    public string GetDtosPath()
        => _configuration["SolutionRootPath"] + _configuration["DtoPath"];

}