namespace Models.RequestHandler;

public class RequestHandlerMetadata
{
    public string[]? Usings { get; set; }
    public string? Namespace { get; set; }
    public string? ClassName { get; set; }
    public List<TypeName>? Constructor { get; set; }
    public List<TypeName>? InjectedRequestClass { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
    public List<Property>? Properties { get; set; }
}