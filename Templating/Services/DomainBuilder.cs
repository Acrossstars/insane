using Core;
using Core.Domain.Common;
using Core.Extensions;
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

    public DomainBuilder(
        IConfiguration configuration,
        string dtosPath,
        string metadataDir,
        string domainEntity,
        DomainDefinition domainDefinition
        )
    {
        _configuration = configuration;
        _dtosPath = dtosPath;
        _domainEntity = domainEntity;
        _metadataDir = metadataDir;
        _domainDefinition = domainDefinition;
        _manyEntities = _domainEntity.Pluralize();

        _solutionRoot = _configuration["SolutionRootPath"]!;
        _domainLayerPath = _configuration["DomainLayerPath"]!;
        _domainLayerNamespaceRoot = _configuration["DomainLayerNamespaceRoot"];
        _applicationLayerPath = _configuration["ApplicationLayerPath"]!;
    }

    public void BuildEntities()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        var entitiesFolderName = $"Entities";

        var internalPath = $"\\{_manyEntities}\\{entitiesFolderName}";

        var outputFilePath = $"{_solutionRoot}{_domainLayerPath}{internalPath}";

        foreach (var item in _domainDefinition.Entities!)
        {
            item.Namespace = $"{_domainLayerNamespaceRoot}{internalPath.Replace("\\", ".")}";

            BuildTools.AppendToBuild(_metadataDir, builderContexts, outputFilePath, item, item.ClassName);
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

        var eventsFolderName = $"Events";
        var eventHadlersFolderName = "EventHandlers";

        var eventsInternalPath = $"\\{_manyEntities}\\{eventsFolderName}";
        var eventHadlersInternalPath = $"\\{_manyEntities}\\{eventHadlersFolderName}";

        var eventsOutputFilePath = $"{_solutionRoot}{_domainLayerPath}{eventsInternalPath}";
        var eventHadlersOutputFilePath = $"{_solutionRoot}{_applicationLayerPath}{eventHadlersInternalPath}";

        var eventsGeneratedNmespace = $"{_domainLayerNamespaceRoot}{eventsInternalPath.Replace("\\", ".")}";
        var eventHandlersGeneratedNmespace = $"{_applicationLayerPath.Split("\\").Last()}{eventHadlersInternalPath.Replace("\\", ".")}";

        foreach (var domainEvent in _domainDefinition.DomainEvents!)
        {
            var eventMetadata = CreateDomainEventMetadata(
                _domainEntity,
                domainEvent.ClassName,
                eventsGeneratedNmespace, domainEvent.Context.OperableProperties,
                domainEvent.Context
                );

            BuildTools.AppendToBuild(_metadataDir, builderContexts, eventsOutputFilePath, eventMetadata, eventMetadata.ClassName);

            var eventHandlerMetadata = CreateDomainEventHandlerMetadata(eventMetadata.ClassName, eventMetadata, eventHandlersGeneratedNmespace);
            BuildTools.AppendToBuild(_metadataDir, builderContexts, eventHadlersOutputFilePath, eventHandlerMetadata, eventHandlerMetadata.ClassName);
        }

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    private DomainEventMetadata CreateDomainEventMetadata(string domainEntity, string domainEvent, string generatedNmespace, List<MetaProperty> properties, DomainEventContext context)
    {
        DomainEventMetadata metadata = new DomainEventMetadata()
        {
            ClassName = $"{domainEvent}Event",
            //no needed perhaps
            FilePath = "",
            Usings = Array.Empty<string>(),
            Namespace = generatedNmespace,

            BaseConstructor = new string[]
                                {
                                    "entityId: id",
                                    $"\"{domainEntity}\""
                                },
            Context = context
        };

        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        AwesomeHelper.FillOperablePropertiesFromMetadata(metadata.Context, metadata);

        return metadata;
    }

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