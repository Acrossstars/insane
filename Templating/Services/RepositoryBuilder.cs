using Core;
using Core.Generation;
using Core.Metadatas.Repositories;
using Templating.Features;
using Templating.Infra;

namespace Templating.Services;

public class RepositoryBuilder : BaseBuilder
{
    private readonly string _outputFileRepository;
    private readonly string? _pathRepository;
    private readonly string? _repositoriesFolderName;
    private readonly string? _manyEntities;
    private readonly string? _pathRepositoryInterface;
    private readonly string? _outputFileRepositoryInterface;

    private readonly RepositoryMetadata _repositoryMetadata;

    public RepositoryBuilder(
        PathNameSpacesService pathNameSpacesService,
        GenerationDesign generationDesign,
        RepositoryMetadata repositoryMetadata) : base(pathNameSpacesService, generationDesign)
    {
        _repositoriesFolderName = generationDesign.RepositoriesFolderName!;
        _repositoryMetadata = repositoryMetadata;

        _manyEntities = _repositoryMetadata.AggregateEntity.Pluralize();

        _pathRepository = $"Infrastructure\\Data\\{_repositoriesFolderName}\\{_manyEntities}\\{_repositoryMetadata.AggregateEntity + "Repository"}";
        _outputFileRepository = $"{_solutionRoot}{_apiRoot}\\{_pathRepository}";

        _pathRepositoryInterface = $"Domain\\Data\\{_repositoriesFolderName}\\{_manyEntities}\\I{_repositoryMetadata.AggregateEntity + "Repository"}";
        _outputFileRepositoryInterface = $"{_solutionRoot}{_apiRoot}\\{_pathRepositoryInterface}";
    }

    public void GenerateRepositoriesMetadata(string metadataDir, RepositoryMetadata baseRepositoryMetadata)
    {
        var builderContexts = new List<ObjectBuilderContext>();

        GenerateRepository(metadataDir, baseRepositoryMetadata, builderContexts);
        GenerateRepositoryInterface(metadataDir, baseRepositoryMetadata, builderContexts);

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    private void GenerateRepository(string metadataDir, RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        repositoryMetadata.Type = MetadataType.PostgreSqlRepository;
        repositoryMetadata.InterfaceName = $"I{repositoryMetadata.AggregateEntity}Repository"; // все эти костыли УБРАТЬ.
        repositoryMetadata.Namespace = BuildNamepace(repositoryMetadata.Type);

        foreach (var method in repositoryMetadata.Methods)
        {
            if (!string.IsNullOrEmpty(method.Type))
            {
                method.Type = $"<{method.Type}>";
            }
        }

        BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFileRepository, repositoryMetadata, repositoryMetadata.AggregateEntity);

        
    }

    private void GenerateRepositoryInterface(string metadataDir, RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        repositoryMetadata.Type = MetadataType.IRepository;
        repositoryMetadata.Namespace = BuildNamepace(repositoryMetadata.Type);

        BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFileRepositoryInterface, repositoryMetadata, repositoryMetadata.AggregateEntity);
    }

    private string BuildNamepace(MetadataType metadataType)
    {
        string layerPath = string.Empty;

        switch (metadataType)
        {
            case MetadataType.PostgreSqlRepository:
                layerPath = _pathNameSpacesService
                    .GetInfrastructurePath()
                    .Split('\\')
                    .Last(); // Infrastructure
                break;
            case MetadataType.IRepository:
                layerPath = _pathNameSpacesService
                    .GetDomainLayerPath()
                    .Split('\\')
                    .Last(); // BoundedContext / Domain
                break;
        }

        var dataFolderName = _generationDesign.InfrastructureDataFolderName; // Data

        // Infrastructure.Data.Repositories.SomeAggregate || Domain.Data.Repositories.SomeAggregate.
        var nameSpace = $"{layerPath}.{dataFolderName}.{_repositoriesFolderName}.{_manyEntities}";

        return nameSpace;
    }

}
