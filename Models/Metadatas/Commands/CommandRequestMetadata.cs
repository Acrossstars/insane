using Core.Domain.Common;

namespace Core.Metadatas.Commands;

public class CommandRequestMetadata : YetAnotherBaseMetadata /*BaseMetadata*/
{
    public CommandRequestMetadata()
    {
        Type = MetadataType.CommandRequest;
    }
}