﻿using Core;
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
    private readonly DomainDefinition _domainDefinition;
    private readonly RepositoryMetadata _repositoryMetadata;

    public RepositoryBuilder(
        PathNameSpacesService pathNameSpacesService,
        GenerationDesign generationDesign,
        RepositoryMetadata repositoryMetadata,
        DomainDefinition domainDefinition
        ) : base(pathNameSpacesService, generationDesign)
    {
        _repositoriesFolderName = generationDesign.RepositoriesFolderName!;
        _domainDefinition = domainDefinition;
        _repositoryMetadata = repositoryMetadata;

        _manyEntities = _repositoryMetadata.AggregateEntity.Pluralize();

        var repositoryPaths = pathNameSpacesService.GetRepositoriesPathConfig(_manyEntities);

        _outputFileRepository = repositoryPaths.RepositoryOutputFilePath;

        _outputFileRepositoryInterface = repositoryPaths.RepositoryInterfaceOutputFilePath;
    }

    public void GenerateRepositoriesMetadata(string metadataDir, RepositoryMetadata baseRepositoryMetadata)
    {
        var builderContexts = new List<ObjectBuilderContext>();

        GenerateRepositoryInterface(metadataDir, baseRepositoryMetadata, builderContexts);
        GenerateRepository(metadataDir, baseRepositoryMetadata, builderContexts);

        foreach (var builderMetadata in builderContexts)
        {
            var builder = new FileBuilder();
            builder.Build(builderMetadata);
        }
    }

    private void GenerateRepository(string metadataDir, RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        var baseRepositoryNamespace = "Subject.BuildingBlocks.Infrastructure.PostgreSQL";
        var dbContextNamespace = "Infrastructure.Data.DbContext";

        var entityNamespace = _domainDefinition.Entities.First().Namespace;
        var repositoryInterfaceNamespace = _domainDefinition.RepositoryInterfaces.First().Namespace;

        repositoryMetadata.Usings = new string[] { baseRepositoryNamespace, dbContextNamespace, repositoryInterfaceNamespace, entityNamespace };

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

        BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFileRepository, repositoryMetadata, $"{repositoryMetadata.AggregateEntity}Repository");



    }

    private void GenerateRepositoryInterface(string metadataDir, RepositoryMetadata baseRepositoryMetadata, List<ObjectBuilderContext> builderContexts)
    {
        var repositoryMetadata = new RepositoryMetadata(baseRepositoryMetadata);

        var baseRepositoryNamespace = "Subject.BuildingBlocks.Domain.Data";

        var entityNamespace = _domainDefinition.Entities.First().Namespace;

        repositoryMetadata.Usings = new string[] { baseRepositoryNamespace, entityNamespace };

        repositoryMetadata.Type = MetadataType.IRepository;
        repositoryMetadata.Namespace = BuildNamepace(repositoryMetadata.Type);

        _domainDefinition.RepositoryInterfaces.Add(repositoryMetadata);

        BuildTools.AppendToBuild(metadataDir, builderContexts, _outputFileRepositoryInterface, repositoryMetadata, $"I{repositoryMetadata.AggregateEntity}Repository");
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

        var dataFolderName = _generationDesign.RepositoriesDataFolderName; // Data

        // Infrastructure.Data.Repositories.SomeAggregate || Domain.Data.Repositories.SomeAggregate.
        var nameSpace = $"{layerPath}.{dataFolderName}.{_repositoriesFolderName}.{_manyEntities}";

        return nameSpace;
    }

}
