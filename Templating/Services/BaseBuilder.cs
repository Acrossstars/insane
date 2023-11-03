using Core.Generation;

namespace Templating.Services;

public class BaseBuilder
{
    protected readonly PathNameSpacesService _pathNameSpacesService;
    protected readonly GenerationDesign _generationDesign;

    protected string? _solutionRoot;
    protected string? _apiRoot;

    public BaseBuilder(PathNameSpacesService pathNameSpacesService, GenerationDesign generationDesign)
    {
        _solutionRoot = pathNameSpacesService.GetSolutionRootPath();
        _apiRoot = pathNameSpacesService.GetApiRootPath();

        _pathNameSpacesService = pathNameSpacesService;
        _generationDesign = generationDesign;
    }
}
