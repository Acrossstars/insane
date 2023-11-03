using Core;
using Core.Domain.Common;
using Core.Domain.UseCases;
using Core.Generation;
using Core.Metadatas;
using System;
using Templating.Features;
using Templating.Infra;

namespace Templating.Services;

public class DomainBuilder
{
    private readonly IConfiguration _configuration;

    private string _domainEntity;
    private string _dtosPath;
    private string _metadataDir;
    private DomainDefinition _domainDefinition;
    private string _manyEntities;

    private readonly PathNameSpacesService _pathService;
    private readonly GenerationDesign _generationDesign;

    private string _solutionRoot;
    private string _domainLayerPath;
    private string? _domainLayerNamespaceRoot;
    private string _eventsOutputFilePath;
    private string _eventHadlersOutputFilePath;
    private string _eventsGeneratedNmespace;
    private string _eventHandlersGeneratedNmespace;

    public DomainBuilder(
        IConfiguration configuration,
        string metadataDir,
        PathNameSpacesService pathService,
        GenerationDesign generationDesign,
        DomainDefinition domainDefinition
        )
    {
        _configuration = configuration;
        _metadataDir = metadataDir;
        _domainEntity = domainDefinition.Domain!;
        _domainDefinition = domainDefinition;
        _manyEntities = _domainEntity.Pluralize();

        _pathService = pathService;
        _generationDesign = generationDesign;
        _solutionRoot = _pathService.GetSolutionRootPath();
        _domainLayerPath = _pathService.GetDomainLayerPath();
        _domainLayerNamespaceRoot = _pathService.GetDomainLayerNamespaceRoot();
        _dtosPath = _pathService.GetDtosPath();

        var eventPath = _pathService.GetEventsPathConfig(_manyEntities);

        _eventsOutputFilePath = eventPath.EventsOutputFilePath!;
        _eventHadlersOutputFilePath = eventPath.EventHandlersOutputFilePath!;

        (_eventsGeneratedNmespace, _eventHandlersGeneratedNmespace) = _pathService.GetEventsGeneratedNamespace(_manyEntities);
    }

    public void BuildEntities()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        var internalPath = $"\\{_manyEntities}\\{_generationDesign.EntitiesFolderName}";

        var outputFilePath = $"{_solutionRoot}{_domainLayerPath}{internalPath}";

        foreach (var metadata in _domainDefinition.Entities!)
        {
            metadata.BaseEntities = new List<string>()
            {
                "BaseEntity"
            };

            //ультракостыль
            metadata.OperableProperties = metadata.Properties;

            metadata.Namespace = $"{_pathService.GetDomainLayerNamespaceRoot()}{internalPath.Replace("\\", ".")}";

            metadata.Usings = new string[] { };
            metadata.Constructor = new List<TypeName>();
            metadata.Properties = new List<MetaProperty>();
            metadata.InjectedProperties = new List<InjectedProperty>();

            AwesomeHelper.FillOperablePropertiesFromMetadata(metadata, metadata);

            //тож костыль но менее критичный
            metadata.CreateParameters = metadata.Constructor;

            BuildTools.AppendToBuild(_metadataDir, builderContexts, outputFilePath, metadata, metadata.ClassName!);
        }

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    public void BuildValueObjects()
    {
        foreach (var item in _domainDefinition.ValueObjects!)
        {

        }
    }

    public void BuildEvents()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        foreach (var domainEvent in _domainDefinition.Events!)
        {
            var metadata = new DomainEventMetadata
            {
                BaseEntities = new List<string>()
                {
                    "DomainEvent"
                },
                ClassName = string.Format(_generationDesign.EventСlassNamePattern!, domainEvent.ClassName),
                //no needed perhaps
                FilePath = "",
                Usings = Array.Empty<string>(),
                Namespace = _eventsGeneratedNmespace,

                BaseConstructor = new string[]
                {
                    "entityId: id",
                    $"\"{_domainEntity}\""
                },
                Context = new()
                {
                    OperableProperties = domainEvent.Properties
                },
                Constructor = new List<TypeName>(),
                Properties = new List<MetaProperty>(),
                InjectedProperties = new List<InjectedProperty>()
            };

            AwesomeHelper.FillOperablePropertiesFromMetadata(metadata.Context, metadata);

            BuildTools.AppendToBuild(_metadataDir, builderContexts, _eventsOutputFilePath, metadata, metadata.ClassName);

            var eventHandlerMetadata = CreateDomainEventHandlerMetadata(metadata.ClassName, metadata, _eventHandlersGeneratedNmespace);

            BuildTools.AppendToBuild(_metadataDir, builderContexts, _eventHadlersOutputFilePath, eventHandlerMetadata, eventHandlerMetadata.ClassName!);
        }

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    public void BuildUseCases()
    {
        foreach (var useCase in _domainDefinition.UseCases!)
        {
            var metaUseCase = new MetaUseCase(
                useCase.DomainEntityName,
                useCase.Name,
                useCase.RequestType,
                useCase.HttpMethodType,
                useCase.UseCaseContext,
                useCase.UseCaseSteps,
                useCase.HasRestEndpoint);

            var userCasesBuilder = new UserCasesBuilder(_configuration, _domainDefinition, metaUseCase, _generationDesign, _pathService);
            //var userCasesBuilder = new UserCasesBuilder(_configuration, _domainDefinition, useCase);

            userCasesBuilder.GenerateUseCase(_domainEntity, _dtosPath, _metadataDir);
        }
    }

    private DomainEventHandlerMetadata CreateDomainEventHandlerMetadata(string domainEvent, DomainEventMetadata eventMetadata, string generatedNmespace)
    {
        DomainEventHandlerMetadata metadata = new DomainEventHandlerMetadata()
        {
            BaseEntities = new List<string>()
            {
                //"INotificationHandler<{{ event_class_name }}>",
                string.Format(_generationDesign.EventHandlerBaseСlassNamePattern!, domainEvent)
            },
            ClassName = string.Format(_generationDesign.EventHandlerСlassNamePattern!, domainEvent),

            EventClassName = eventMetadata.ClassName!,
            //no needed perhaps
            FilePath = "",
            Usings = new string[]
            {
                eventMetadata.Namespace!
            },
            Namespace = generatedNmespace,
            BaseConstructor = new string[]
            {
                "entityId: id",
                $"entityType: $\"{eventMetadata.Context.DomainEntityName}\""
            },
        };

        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();

        metadata.PrivateFields = new List<TypeName>()
        {
            new TypeName("private readonly ILogger", "_logger")
        };

        metadata.InjectedInfrastructure = new List<TypeName>()
        {
            new TypeName("ILogger", "logger")
        };

        metadata.InjectedProperties = new List<InjectedProperty>()
        {
        };

        return metadata;
    }

    public void BuildPostgreSqlRepository()
    {
        foreach (var repository in _domainDefinition.Repositories)
        {
            var repositoryBuilder = new RepositoryBuilder(_pathService, _generationDesign, repository);

            repositoryBuilder.GenerateRepositoriesMetadata(_metadataDir, repository);
            
        }
    }

    public void BuildSpecifications()
    {

    }

    public void BuildServices()
    {

    }
}