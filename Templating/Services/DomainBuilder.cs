using Core;
using Core.Domain;
using Core.Models;
using Core.Models.Common;
using Humanizer;
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

        foreach (var item in _domainDefinition.Entities!)
        {
            var eventsFolderName = $"Entities";

            var path = $"\\Domain\\BoundedContexts\\{_manyEntities}\\{eventsFolderName}";

            string? solutionRoot = _configuration["SolutionRootPath"];

            string? apiRoot = _configuration["ApiRootPath"];

            var outputFilePath = $"{solutionRoot}{apiRoot}\\{path}";

            var generatedNmespace = $"Domain.BoundedContexts.{_manyEntities}.{eventsFolderName}";

            generatedNmespace = path.Replace("\\",".");

            item.Namespace = generatedNmespace.Remove(0,1);

            //var metadata = CreateDomainEntityMetadata(_domainEntity, domainEvent, generatedNmespace);

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

        foreach (var domainEvent in _domainDefinition.DomainEvents!)
        {

            var eventsFolderName = $"Events";


            var path = $"\\Domain\\BoundedContexts\\{_manyEntities}\\{eventsFolderName}";

            string? solutionRoot = _configuration["SolutionRootPath"];

            string? apiRoot = _configuration["ApiRootPath"];

            var outputFilePath = $"{solutionRoot}{apiRoot}\\{path}";

            //var generatedNmespace = $"Domain.{_manyEntities}.Events";

            var generatedNmespace = path.Replace("\\", ".");
            generatedNmespace = generatedNmespace.Remove(0, 1);

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