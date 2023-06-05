using Core.Domain.Interfaces;

namespace Core.Domain.Common;

public class YetAnotherBaseMetadata : BaseMetadata, IMetaProperties
{
    public List<TypeName>? InjectedRequestClass { get; set; }
    public string? RequestType { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
}