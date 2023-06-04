using Core;
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
    }

    public void BuildEntities()
    {
        var builderContexts = new List<ObjectBuilderContext>();

        var entitiesFolderName = $"Entities";

        var internalPath = $"\\{_manyEntities}\\{entitiesFolderName}";

        string solutionRoot = _configuration["SolutionRootPath"]!;
        string domainLayerPath = _configuration["DomainLayerPath"]!;

        var outputFilePath = $"{solutionRoot}{domainLayerPath}{internalPath}";

        foreach (var item in _domainDefinition.Entities!)
        {
            item.Namespace = $"{_configuration["DomainLayerNamespaceRoot"]}{internalPath.Replace("\\", ".")}";

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

        string solutionRoot = _configuration["SolutionRootPath"]!;
        string domainLayerPath = _configuration["DomainLayerPath"]!;
        string applicationLayerPath = _configuration["ApplicationLayerPath"]!;

        var eventsOutputFilePath = $"{solutionRoot}{domainLayerPath}{eventsInternalPath}";
        var eventHadlersOutputFilePath = $"{solutionRoot}{applicationLayerPath}{eventHadlersInternalPath}";

        var eventsGeneratedNmespace = $"{_configuration["DomainLayerNamespaceRoot"]}{eventsInternalPath.Replace("\\", ".")}";
        var eventHandlersGeneratedNmespace = $"{applicationLayerPath.Split("\\").Last()}{eventHadlersInternalPath.Replace("\\", ".")}";

        foreach (var domainEvent in _domainDefinition.DomainEvents!)
        {
            var eventMetadata = CreateDomainEventMetadata(_domainEntity, domainEvent, eventsGeneratedNmespace);
            BuildTools.AppendToBuild(_metadataDir, builderContexts, eventsOutputFilePath, eventMetadata, eventMetadata.ClassName);

            var eventHandlerMetadata = CreateDomainEventHandlerMetadata(domainEvent, eventMetadata, eventHandlersGeneratedNmespace);
            BuildTools.AppendToBuild(_metadataDir, builderContexts, eventHadlersOutputFilePath, eventHandlerMetadata, eventHandlerMetadata.ClassName);
        }

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    private DomainEventMetadata CreateDomainEventMetadata(string domainEntity, string domainEvent, string generatedNmespace)
    {
        return new DomainEventMetadata()
        {
            ClassName = $"{domainEvent}Event",
            //no needed perhaps
            FilePath = "",
            Usings = new string[]
                        {

                        },
            Namespace = generatedNmespace,
            Properties = new List<MetaProperty>()
            {
                //new Property("public","string","Id", new string[]{ "get", "set" }),
                //new Property("public","string","Email", new string[]{ "get", "set" }),
            },
            BaseConstructor = new string[]
                        {
                    "entityId: id",
                    "entityType: DomainMetadata.User"
                        },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "id"),
                //new TypeName("string", "email"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                //new InjectedProperty("Id", "id"),
                //new InjectedProperty("Email", "email"),
            },
        };
    }

    private DomainEventHandlerMetadata CreateDomainEventHandlerMetadata(string domainEvent, DomainEventMetadata eventMetadata, string generatedNmespace)
    {
        return new DomainEventHandlerMetadata()
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
            Properties = new List<MetaProperty>()
            {
                //new Property("public","string","Id", new string[]{ "get", "set" }),
                //new Property("public","string","Email", new string[]{ "get", "set" }),
            },
            BaseConstructor = new string[]
                        {
                    "entityId: id",
                    "entityType: DomainMetadata.User"
                        },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "id"),
                //new TypeName("string", "email"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                //new InjectedProperty("Id", "id"),
                //new InjectedProperty("Email", "email"),
            },
        };
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