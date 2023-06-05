namespace Core.Domain.UseCases;

public class MetaUseCaseStep
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Target { get; set; }
    public string? Logic { get; set; }
    public int Order { get; set; }
    public MetaUseCaseContext? UseCaseContext { get; set; }
}