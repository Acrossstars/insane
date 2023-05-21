namespace Core.Models.Common;

public class YetAnotherBaseMetadata : BaseMetadata
{
    public List<TypeName>? InjectedRequestClass { get; set; }
    public string? RequestType { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
}