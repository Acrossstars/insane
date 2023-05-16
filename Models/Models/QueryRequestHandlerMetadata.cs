using Core.Models.Common;

namespace Core.Models;

public class QueryRequestHandlerMetadata : RequestHandlerBaseMetadata
{
    public string? QueryReturnType { get; set; }
}