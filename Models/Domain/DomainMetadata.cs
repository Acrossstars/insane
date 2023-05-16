namespace Core.Domain;

public class DomainMetadata
{
    public DomainMetadata()
    {

    }

    public List<string>? GlobalUsings { get; set; }
    public List<string>? Aggregates { get; set; }
    public List<string>? ValueObjects { get; set; }
}