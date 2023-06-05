using Core.Domain;
using Core.Domain.Interfaces;

namespace Core.Models;

public class DomainEventMetadata : BaseMetadata, IMetaProperties
{
    public DomainEventMetadata()
    {
        Type = MetadataType.DomainEvent;
    }

    public DomainEventContext Context { get; set; }
}

public class DomainEventHandlerMetadata : BaseMetadata
{
    public DomainEventHandlerMetadata()
    {
        Type = MetadataType.DomainEventHandler;
    }

    public string EventClassName { get; set; }
}