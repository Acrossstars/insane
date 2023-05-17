namespace Core.Models;

public class QueryRequestMetadata : BaseMetadata
{
    public QueryRequestMetadata()
    {
        Type = MetadataType.QueryRequest;
    }

    public string? QueryReturnType { get; set; }
}