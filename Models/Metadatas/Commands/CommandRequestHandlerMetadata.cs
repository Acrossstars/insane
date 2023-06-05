using Core.Domain.Common;

namespace Core.Metadatas.Commands;

public class CommandRequestHandlerMetadata : YetAnotherBaseMetadata
{
    public CommandRequestHandlerMetadata()
    {
        Type = MetadataType.CommandRequestHandler;
    }
}