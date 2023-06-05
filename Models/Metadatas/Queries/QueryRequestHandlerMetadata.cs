using Core.Domain.Common;

namespace Core.Metadatas.Queries;

public class QueryRequestHandlerMetadata : YetAnotherBaseMetadata
{
    public QueryRequestHandlerMetadata()
    {
        Type = MetadataType.QueryRequestHandler;
    }

    public string? QueryReturnType { get; set; }
}