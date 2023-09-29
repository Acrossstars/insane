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
    public List<IRepositoryMetadata>? Repositories { get; set; } // вот это доделать. Пока не работает (чтобы тоже из Жсона все тянулось)
    public List<DomainEventMetadata>? Events { get; set; }
    public List<MetaUseCase>? UseCases { get; set; } // вот это сделать, чтобы ЮзКейсы тоже из жсона подтягивались, а не хардкодились в Програм.
    // по аналогии с ентитями / ивентами.


    public List<string>? GlobalUsings { get; set; } = new List<string>();
    public List<string>? Aggregates { get; set; } = new List<string>();
    public List<string>? ValueObjects { get; set; } = new List<string>();
    public List<string>? IntegrationEvents { get; set; } = new List<string>();
    public List<DtoMetadata> Dtos { get; set; }
    public List<(string source,string destiantion)>? Mappings { get; set; } = new List<(string source, string destiantion)>();
}