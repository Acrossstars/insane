namespace Core.Models;

public class DomainEventMetadata : BaseMetadata
{
    public DomainEventMetadata()
    {
        Type = MetadataType.DomainEvent;
    }
}

public class DomainEventHandlerMetadata : BaseMetadata
{
    public DomainEventHandlerMetadata()
    {
        Type = MetadataType.DomainEventHandler;
    }

    public string EventClassName { get; set; }
}