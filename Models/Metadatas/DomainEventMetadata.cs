namespace Core.Metadatas;

public class DomainEventMetadata : BaseMetadata, IMetaProperties
{
    public DomainEventMetadata()
    {
        Type = MetadataType.DomainEvent;
    }

    public DomainEventContext Context { get; set; }
}