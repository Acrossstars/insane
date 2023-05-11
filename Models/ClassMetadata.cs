namespace Models;

public class ClassMetadata
{
    public string[]? Usings { get; set; }
    public string? Namespace { get; set; }
    public string? ClassName { get; set; }
    public string? RequestClassName { get; set; }
    public string[]? Constructor { get; set; }
    public string[]? BaseConstructor { get; set; }
    public string[]? InjectedInfrastructure { get; set; }
    public string[]? InjectedProperties { get; set; }
    public string[]? Properties { get; set; }
}