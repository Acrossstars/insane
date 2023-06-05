using Core.Domain.Common;
using Core.Domain.Interfaces;

namespace Core.Metadatas.Queries;

public class QueryRequestMetadata : BaseMetadata, IMetaProperties
{
    public QueryRequestMetadata()
    {
        Type = MetadataType.QueryRequest;
    }

    public string? QueryReturnType { get; set; }
}