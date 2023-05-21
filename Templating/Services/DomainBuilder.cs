using Core;
using Core.Domain;
using Core.Models;
using Core.Models.Common;
using Microsoft.Extensions.Configuration;
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

    public DomainBuilder(
        IConfiguration configuration,
        string domainEntity,
        string dtosPath,
        string metadataDir
        )
    {
        _configuration = configuration;
        _dtosPath = dtosPath;
        _domainEntity = domainEntity;
        _metadataDir = metadataDir;
    }

    public DomainBuilder(IConfigurationRoot configuration, string dtosPath, string metadataDir, DomainDefinition domainDefinition)
    {
        _configuration = configuration;
        _dtosPath = dtosPath;
        _metadataDir = metadataDir;
        _domainDefinition = domainDefinition;
    }

    public void BuildEntities()
    {
        foreach (var item in _domainDefinition.Aggregates!)
        {

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

        foreach (var domainEvent in _domainDefinition.DomainEvents!)
        {

            var eventsFolderName = $"Events";

            var manyEntities = $"{_domainEntity}s";

            var path = $"\\Identity\\Domain\\{eventsFolderName}";

            string? solutionRoot = _configuration["SolutionRootPath"];

            string? apiRoot = _configuration["ApiRootPath"];

            var outputFilePath = $"{solutionRoot}{apiRoot}\\{path}";

            var generatedNmespace = $"Identity.Domain.Events";

            var metadata = CreateDomainEventMetadata(_domainEntity, domainEvent, generatedNmespace);

            BuildTools.AppendToBuild(_metadataDir, builderContexts, outputFilePath, metadata, metadata.ClassName);
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
            Properties = new List<Property>()
            {
                new Property("public","string","Id", new string[]{ "get", "set" }),
                new Property("public","string","Email", new string[]{ "get", "set" }),
            },
            BaseConstructor = new string[]
                        {
                    "entityId: id",
                    "entityType: DomainMetadata.User"
                        },
            Constructor = new List<TypeName>()
            {
                new TypeName("string", "id"),
                new TypeName("string", "email"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                new InjectedProperty("Id", "id"),
                new InjectedProperty("Email", "email"),
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