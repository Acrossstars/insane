namespace Core.Models;

public class CommandRequestMetadata : BaseMetadata
{
    public List<TypeName>? InjectedRequestClass { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
}