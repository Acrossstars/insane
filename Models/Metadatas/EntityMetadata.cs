using Core.Domain.Common;

namespace Core.Metadatas;

public class EntityMetadata : BaseMetadata
{
    public EntityMetadata()
    {
        Type = MetadataType.Entity;
    }
}