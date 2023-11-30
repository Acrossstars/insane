﻿using Core;
using Core.Domain.UseCases;
using Core.Generation;
using Core.Metadatas;
using Templating.Features;

namespace Templating.Services;

public class UserCasesBuilder
{
    private readonly DomainDefinition _domainDefinition;
    private MetaUseCase _useCase;
    private readonly GenerationDesign _generationDesign;
    private string _outputFilePath;
    private readonly MetadatasBuilder _metadataBuilder;

    private readonly PathService _pathService;
    private readonly NamespaceService _namespaceService;
    private readonly BuildTools _buildTools;

    public UserCasesBuilder(
        DomainDefinition domainDefinition,
        MetaUseCase useCase,
        GenerationDesign generationDesign,
        PathService pathService,
        NamespaceService namespaceService,
        BuildTools buildTools
        )
    {
        _domainDefinition = domainDefinition;
        _useCase = useCase;
        _generationDesign = generationDesign;

        _pathService = pathService;
        _namespaceService = namespaceService;
        _buildTools = buildTools;
        _useCase.ManyEntities = _useCase.DomainEntityName.Pluralize();

        _useCase.UseCaseNamespace = _namespaceService.CreateUseCaseNamespace(_useCase.ManyEntities, _useCase.Name);
        _outputFilePath = _pathService.CreateUseCasePath(_useCase.ManyEntities, _useCase.Name);
            
        _metadataBuilder = new MetadatasBuilder(_generationDesign, _domainDefinition);
    }

    //TODO: Rename Namespace to Match Folder Structure
    public List<ObjectBuilderContext> GenerateUseCase()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        switch (_useCase.RequestType)
        {
            case RequestType.Command:

                var commandRequest = _metadataBuilder.CreateMetaCommandRequest(_useCase);

                _buildTools.AppendToBuild(builderContexts, _outputFilePath, commandRequest, commandRequest.ClassName!);

                var commandRequestHandler = _metadataBuilder.CreateCommandRequestHandlerMetadata(_useCase);

                _buildTools.AppendToBuild(builderContexts, _outputFilePath, commandRequestHandler, commandRequestHandler.ClassName!);

               // var commandRestEndpoint = MetadatasBuilder.

                break;
            case RequestType.Query:
                //TODO: for query handler needed to add using that contains namespace for query request
                var queryRequest = _metadataBuilder.CreateQueryRequestMetadata(_useCase);

                _buildTools.AppendToBuild(builderContexts, _outputFilePath, queryRequest, queryRequest.ClassName!);

                var queryRequestHandler = _metadataBuilder.CreateQueryRequestHandlerMetadata(_useCase);

                _buildTools.AppendToBuild(builderContexts, _outputFilePath, queryRequestHandler, queryRequestHandler.ClassName!);
                break;
        }

        var dto = MetadatasBuilder.GetDto(_useCase, _useCase.UseCaseNamespace);

        _useCase.DtoMetadata = dto;

        var dtoPath = _pathService.CreateDtosPath(_useCase.ManyEntities);
        _buildTools.AppendToBuild(builderContexts, dtoPath, dto, dto.ClassName!);

        if (_useCase.HasRestEndpoint)
        {
            RestEndpointMetadata restEndpoint = 
                _metadataBuilder.CreateRestEndpointMetadata(_useCase, _useCase.UseCaseNamespace);

            _buildTools.AppendToBuild(builderContexts, _outputFilePath, restEndpoint, restEndpoint.ClassName!);
        }

        return builderContexts;
    }
}