using Core;
using Core.Domain.Common;
using Core.Metadatas;
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

    private string _solutionRoot;
    private string _domainLayerPath;
    private string? _domainLayerNamespaceRoot;
    private string _applicationLayerPath;

    private string _eventsFolderName;
    private string _eventHadlersFolderName;
    private string _eventsInternalPath;
    private string _eventHadlersInternalPath;
    private string _eventsOutputFilePath;
    private string _eventHadlersOutputFilePath;
    private string _eventsGeneratedNmespace;
    private string _eventHandlersGeneratedNmespace;

    public DomainBuilder(
        IConfiguration configuration,
        string metadataDir,
        string domainEntity,
        DomainDefinition domainDefinition
        )
    {
        _configuration = configuration;
        _metadataDir = metadataDir;
        _domainEntity = domainDefinition.Domain;
        //_domainEntity = domainEntity;
        _domainDefinition = domainDefinition;
        _manyEntities = _domainEntity.Pluralize();

        _solutionRoot = _configuration["SolutionRootPath"]!;
        _domainLayerPath = _configuration["DomainLayerPath"]!;
        _domainLayerNamespaceRoot = _configuration["DomainLayerNamespaceRoot"];
        _applicationLayerPath = _configuration["ApplicationLayerPath"]!;
        _dtosPath = _configuration["SolutionRootPath"] + _configuration["DtoPath"];

        _eventsFolderName = $"Events";
        _eventHadlersFolderName = "EventHandlers";

        _eventsInternalPath = $"\\{_manyEntities}\\{_eventsFolderName}";
        _eventHadlersInternalPath = $"\\{_manyEntities}\\{_eventHadlersFolderName}";

        _eventsOutputFilePath = $"{_solutionRoot}{_domainLayerPath}{_eventsInternalPath}";
        _eventHadlersOutputFilePath = $"{_solutionRoot}{_applicationLayerPath}{_eventHadlersInternalPath}";

        _eventsGeneratedNmespace = $"{_domainLayerNamespaceRoot}{_eventsInternalPath.Replace("\\", ".")}";

        _eventHandlersGeneratedNmespace = $"{_applicationLayerPath.Split("\\").Last()}{_eventHadlersInternalPath.Replace("\\", ".")}";
    }

    public void BuildEntities()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        var entitiesFolderName = $"Entities";

        var internalPath = $"\\{_manyEntities}\\{entitiesFolderName}";

        var outputFilePath = $"{_solutionRoot}{_domainLayerPath}{internalPath}";

        foreach (var metadata in _domainDefinition.Entities!)
        {
            //ультракостыль
            metadata.OperableProperties = metadata.Properties;

            metadata.Namespace = $"{_domainLayerNamespaceRoot}{internalPath.Replace("\\", ".")}";

            metadata.Usings = new string[] { };
            metadata.Constructor = new List<TypeName>();
            metadata.Properties = new List<MetaProperty>();
            metadata.InjectedProperties = new List<InjectedProperty>();

            AwesomeHelper.FillOperablePropertiesFromMetadata(metadata, metadata);

            //тож костыль но менее критичный
            metadata.CreateParameters = metadata.Constructor;

            BuildTools.AppendToBuild(_metadataDir, builderContexts, outputFilePath, metadata, metadata.ClassName);
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
            DomainEventMetadata metadata = new DomainEventMetadata()
            {
                ClassName = $"{domainEvent.ClassName}Event",
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
                }
            };

            metadata.Constructor = new List<TypeName>();
            metadata.Properties = new List<MetaProperty>();
            metadata.InjectedProperties = new List<InjectedProperty>();

            AwesomeHelper.FillOperablePropertiesFromMetadata(metadata.Context, metadata);

            BuildTools.AppendToBuild(_metadataDir, builderContexts, _eventsOutputFilePath, metadata, metadata.ClassName);

            var eventHandlerMetadata = CreateDomainEventHandlerMetadata(metadata.ClassName, metadata, _eventHandlersGeneratedNmespace);

            BuildTools.AppendToBuild(_metadataDir, builderContexts, _eventHadlersOutputFilePath, eventHandlerMetadata, eventHandlerMetadata.ClassName);
        }

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    public void BuildUseCases()
    {
        foreach (var useCase in _domainDefinition.UseCases)
        {
            //var userCasesBuilder = new UserCasesBuilder(_configuration, _domainDefinition, useCase);

            //userCasesBuilder.GenerateUseCase(_configuration, _domainEntity, _dtosPath, _metadataDir);
        }
    }

    //private DomainEventMetadata CreateDomainEventMetadata(string domainEntity, string domainEvent, string generatedNmespace, List<MetaProperty> properties, DomainEventContext context)
    //{


    //    return metadata;
    //}

    private DomainEventHandlerMetadata CreateDomainEventHandlerMetadata(string domainEvent, DomainEventMetadata eventMetadata, string generatedNmespace)
    {
        DomainEventHandlerMetadata metadata = new DomainEventHandlerMetadata()
        {
            ClassName = $"{domainEvent}EventHandler",
            EventClassName = eventMetadata.ClassName,
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
                            $"entityType:           $\"{eventMetadata.Context.DomainEntityName}\""
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

        metadata.InjectedProperties = new List<InjectedProperty>();
        {
            new InjectedProperty("_logger", "logger");
        }

        return metadata;
    }

    public void BuildRepository()
    {

    }

    public void BuildSpecifications()
    {

    }

    public void BuildServices()
    {

    }
}