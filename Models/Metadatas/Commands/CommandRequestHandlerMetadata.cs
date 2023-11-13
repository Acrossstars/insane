namespace Core.Metadatas.Commands;

public class CommandRequestHandlerMetadata : YetAnotherBaseMetadata, ISteppable
{
    public CommandRequestHandlerMetadata()
    {
        Type = MetadataType.CommandRequestHandler;
    }

    public List<string>? Steps { get; set; } = default!;
    public CrudType OperationType { get; set; }
    public object HandlerBody { get; set; }
}