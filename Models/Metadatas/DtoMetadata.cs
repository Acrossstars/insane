using Core.Domain.Common;

namespace Core.Metadatas;

public class DtoMetadata : BaseMetadata
{
    public DtoMetadata()
    {
        Type = MetadataType.Dto;
    }
}