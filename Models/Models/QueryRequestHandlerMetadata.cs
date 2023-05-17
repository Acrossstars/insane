namespace Core.Models;

public class QueryRequestHandlerMetadata : YetAnotherBaseMetadata
{
    public QueryRequestHandlerMetadata()
    {
        Type = MetadataType.QueryRequestHandler;
    }

    public string? QueryReturnType { get; set; }
}