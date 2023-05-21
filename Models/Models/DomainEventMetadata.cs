namespace Core.Models;

public class DomainEventMetadata : BaseMetadata
{
    public DomainEventMetadata()
    {
        Type = MetadataType.DomainEvent;
    }
}