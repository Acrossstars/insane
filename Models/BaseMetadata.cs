namespace Core;

public class BaseMetadata
{
    public string? FilePath { get; set; }
    public string[]? Usings { get; set; }
    public string? Namespace { get; set; }
    public string? ClassName { get; set; }
    public List<TypeName>? Constructor { get; set; }
    public List<Property>? Properties { get; set; }
}