using Core;
using Core.Generation;
using Core.Metadatas.Repositories;
using Templating.Features;
using Templating.Infra;

namespace Templating.Services;

public class RepositoryBuilder
{
    private readonly string _outputFileRepository;
    private readonly string? _manyEntities;
    private readonly string? _outputFileRepositoryInterface;
    private readonly DomainDefinition _domainDefinition;
    private readonly BuildTools _buildTools;
    private readonly RepositoryMetadata _repositoryMetadata;

    private readonly NamespaceService _namespaceService;

    public RepositoryBuilder(
        PathService pathService,
        NamespaceService namespaceService,
        RepositoryMetadata repositoryMetadata,
        DomainDefinition domainDefinition,
        BuildTools buildTools
        )
    {
        _domainDefinition = domainDefinition;
        _buildTools = buildTools;
        _repositoryMetadata = repositoryMetadata;

        _manyEntities = _repositoryMetadata.AggregateEntity.Pluralize();

        _namespaceService = namespaceService;

        _outputFileRepository = pathService.CreateRepositoryPath(_manyEntities);
        _outputFileRepositoryInterface = pathService.CreateRepositoryInterfasePath(_manyEntities);
    }

    public void GenerateRepositoriesMetadata(string metadataDir, RepositoryMetadata baseRepositoryMetadata)
    {
        var builderContexts = new List<ObjectBuilderContext>();

        GenerateRepositoryInterface(metadataDir, baseRepositoryMetadata, builderContexts);
        GenerateRepository(baseRepositoryMetadata, builderContexts);

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    private void GenerateRepository(RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        var baseRepositoryNamespace = "Subject.BuildingBlocks.Infrastructure.PostgreSQL";
        var dbContextNamespace = "Infrastructure.Data.DbContext";

        var entityNamespace = _domainDefinition.Entities.First().Namespace;
        var repositoryInterfaceNamespace = _domainDefinition.RepositoryInterfaces.First().Namespace;

        repositoryMetadata.Usings = new string[] { baseRepositoryNamespace, dbContextNamespace, repositoryInterfaceNamespace, entityNamespace };

        repositoryMetadata.Type = MetadataType.PostgreSqlRepository;
        repositoryMetadata.InterfaceName = $"I{repositoryMetadata.AggregateEntity}Repository"; // все эти костыли УБРАТЬ.
        //repositoryMetadata.Namespace = BuildNamepace(repositoryMetadata.Type);
        repositoryMetadata.Namespace = _namespaceService.CreateRepsitoriesNamespace(_manyEntities);

        foreach (var method in repositoryMetadata.Methods)
        {
            if (!string.IsNullOrEmpty(method.Type))
            {
                method.Type = $"<{method.Type}>";
            }
        }

        _buildTools.AppendToBuild(builderContexts, _outputFileRepository, repositoryMetadata, $"{repositoryMetadata.AggregateEntity}Repository");
    }

    private void GenerateRepositoryInterface(string metadataDir, RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        var baseRepositoryNamespace = "Subject.BuildingBlocks.Domain.Data";

        var entityNamespace = _domainDefinition.Entities.First().Namespace;

        repositoryMetadata.Usings = new string[] { baseRepositoryNamespace, entityNamespace };

        repositoryMetadata.Type = MetadataType.IRepository;
        repositoryMetadata.Namespace = _namespaceService.CreateRepositoryInterfacesNamespace(_manyEntities);

        _domainDefinition.RepositoryInterfaces.Add(repositoryMetadata);

        _buildTools.AppendToBuild(builderContexts, _outputFileRepositoryInterface, repositoryMetadata, $"I{repositoryMetadata.AggregateEntity}Repository");
    }
}
