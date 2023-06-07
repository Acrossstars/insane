using Core.Domain.Common;

namespace Core.Metadatas;

public class DomainEventHandlerMetadata : YetAnotherBaseMetadata
{
    public DomainEventHandlerMetadata()
    {
        Type = MetadataType.DomainEventHandler;
    }

    public string EventClassName { get; set; }
}