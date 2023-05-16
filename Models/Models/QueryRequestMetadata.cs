namespace Core.Models;

public class QueryRequestMetadata : BaseMetadata
{
    public string? QueryReturnType { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
}