namespace Core.Metadatas.Queries;

public class QueryRequestHandlerMetadata : YetAnotherBaseMetadata, ISteppable
{
    public QueryRequestHandlerMetadata()
    {
        Type = MetadataType.QueryRequestHandler;
    }

    public string? QueryReturnType { get; set; }
    public List<string>? Steps { get; set; } = default!;
}