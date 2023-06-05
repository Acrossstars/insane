﻿using Core.Domain.UseCases;
using Core.Metadatas;

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
    public List<DomainEventMetadata>? DomainEvents { get; set; }
    public List<EntityMetadata>? Entities { get; set; }
    public List<MetaUseCase>? UseCases { get; set; }
    public List<DtoMetadata> Dtos { get; set; }
    public List<(string source,string destiantion)>? Mappings { get; set; } = new List<(string source, string destiantion)>();
}