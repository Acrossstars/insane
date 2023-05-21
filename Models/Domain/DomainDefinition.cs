namespace Core.Domain;

public class DomainDefinition
{
    public DomainDefinition()
    {

    }

    public List<string>? GlobalUsings { get; set; } = new List<string>();
    public List<string>? Aggregates { get; set; } = new List<string>();
    public List<string>? ValueObjects { get; set; } = new List<string>();
    public List<string>? IntegrationEvents { get; set; } = new List<string>();
    public List<string>? DomainEvents { get; set; } = new List<string>();
    public List<string>? UseCases { get; set; } = new List<string>();
    public List<string>? Dtos { get; set; } = new List<string>();
}