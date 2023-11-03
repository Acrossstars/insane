using Core.Domain.UseCases;
using Core.Metadatas;
using Core.Metadatas.Repositories;

namespace Core.Domain;

public class DomainDefinition
{
    public DomainDefinition()
    {

    }

    public string? Domain { get; set; }
    public List<EntityMetadata>? Entities { get; set; }
    public List<RepositoryMetadata>? Repositories { get; set; }
    public List<IRepositoryMetadata>? RepositoryInterfaces { get; set; }
    public List<DomainEventMetadata>? Events { get; set; }
    public List<MetaUseCase>? UseCases { get; set; }
 


    public List<string>? GlobalUsings { get; set; } = new List<string>();
    public List<string>? Aggregates { get; set; } = new List<string>();
    public List<string>? ValueObjects { get; set; } = new List<string>();
    public List<string>? IntegrationEvents { get; set; } = new List<string>();
    public List<DtoMetadata> Dtos { get; set; }
    public List<(string source,string destiantion)>? Mappings { get; set; } = new List<(string source, string destiantion)>();
}