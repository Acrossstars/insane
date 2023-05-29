namespace Core.Models;

public class QueryRequestMetadata : YetAnotherBaseMetadata /*BaseMetadata*/
{
    public QueryRequestMetadata()
    {
        Type = MetadataType.QueryRequest;
    }

    public string? QueryReturnType { get; set; }
}