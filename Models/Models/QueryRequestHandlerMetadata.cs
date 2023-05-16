namespace Core.Models;

public class QueryRequestHandlerMetadata : BaseMetadata
{
    public string? RequestClassName { get; set; }
    public string? QueryReturnType { get; set; }
    public List<TypeName>? InjectedRequestClass { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
}