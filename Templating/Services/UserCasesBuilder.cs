using Core;
using Core.Domain.UseCases;
using Core.Generation;
using Core.Metadatas;
using Templating.Features;
using Templating.Infra;

namespace Templating.Services;

public class UserCasesBuilder
{
    private static string _useCasesFolderName;
    private static string _manyEntities;
    private static string? _solutionRoot;
    private static string? _apiRoot;
    private static string _useCaseNamespace;
    private readonly DomainDefinition _domainDefinition;
    private MetaUseCase _useCase;
    private readonly GenerationDesign _generationDesign;
    private readonly PathNameSpacesService _pathNameSpacesService;
    private string _path;
    private string _outputFilePath;
    private readonly MetadatasBuilder _metadataBuilder;

    public UserCasesBuilder(
        IConfiguration configuration,
        DomainDefinition domainDefinition,
        MetaUseCase useCase,
        GenerationDesign generationDesign,
        PathNameSpacesService pathNameSpacesService
        )
    {
        _solutionRoot = pathNameSpacesService.GetSolutionRootPath();
        _apiRoot = pathNameSpacesService.GetApiRootPath();

        _useCasesFolderName = generationDesign.UseCasesFolderName!;
        _domainDefinition = domainDefinition;
        _useCase = useCase;

        _generationDesign = generationDesign;
        _pathNameSpacesService = pathNameSpacesService;

        _manyEntities = _useCase.DomainEntityName.Pluralize();
        
        _useCaseNamespace = $"UseCases.{_manyEntities}.{_useCase.Name}";
        _path = $"\\{_useCasesFolderName}\\{_manyEntities}\\{_useCase.Name}";

        _outputFilePath = $"{_solutionRoot}{_apiRoot}\\{_path}";

        _metadataBuilder = new MetadatasBuilder(_generationDesign, _pathNameSpacesService);
    }

    //TODO: Rename Namespace to Match Folder Structure
    public void GenerateUseCase(string domainEntity, string dtosPath, string metadataDir)
    {
        var builderContexts = new List<ObjectBuilderContext>();

        switch (_useCase.RequestType)
        {
            case RequestType.Command:

                var commandRequest = _metadataBuilder.CreateMetaCommandRequest(_useCase, _useCaseNamespace);

                BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFilePath, commandRequest, commandRequest.ClassName!);

                var commandRequestHandler = _metadataBuilder.CreateCommandRequestHandlerMetadata(_useCase, _useCaseNamespace);

                BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFilePath, commandRequestHandler, commandRequestHandler.ClassName!);

               // var commandRestEndpoint = MetadatasBuilder.

                break;
            case RequestType.Query:
                //TODO: for query handler needed to add using that contains namespace for query request
                var queryRequest = _metadataBuilder.CreateQueryRequestMetadata(_useCase, _useCaseNamespace);

                BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFilePath, queryRequest, queryRequest.ClassName!);

                var queryRequestHandler = _metadataBuilder.CreateQueryRequestHandlerMetadata(_useCase, _useCaseNamespace);

                BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFilePath, queryRequestHandler, queryRequestHandler.ClassName!);
                break;
        }

        var dto = MetadatasBuilder.GetDto(_useCase, _useCaseNamespace);

        _useCase.DtoMetadata = dto;

        var dtoPath = dtosPath + $"\\{domainEntity.Pluralize()}";
        BuildTools.AppendToBuild(metadataDir, builderContexts, dtoPath, dto, dto.ClassName!);

        if (_useCase.HasRestEndpoint)
        {
            RestEndpointMetadata restEndpoint = _metadataBuilder.CreateRestEndpointMetadata(domainEntity, _useCase, _useCaseNamespace);

            BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFilePath, restEndpoint, restEndpoint.ClassName!);

        }



        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }
}