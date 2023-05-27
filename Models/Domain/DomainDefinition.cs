using Core.Models;

namespace Core.Domain;

public class DomainDefinition
{
    public DomainDefinition()
    {

    }

    public List<string>? GlobalUsings { get; set; } = new List<string>();
    public List<string>? Aggregates { get; set; } = new List<string>();
    public List<string>? ValueObjects { get; set; } = new List<string>();
    public List<string>? IntegrationEvents { get; set; } = new List<string>();
    public List<string>? DomainEvents { get; set; } = new List<string>();
    public List<EntityMetadata>? Entities { get; set; }
    public List<UseCase>? UseCases { get; set; }
    public List<DtoMetadata> Dtos { get; set; }
    public List<(string source,string destiantion)>? Mappings { get; set; } = new List<(string source, string destiantion)>();
}