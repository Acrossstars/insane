namespace Core.Domain.UseCases;

public class MetaUseCaseContext : IDataContext
{
    public MetaUseCaseContext()
    {

    }

    public string? DomainEntityName { get; set; } = default!;
    public string? ManyEntities { get; set; } = default!;
    public string? Input { get; set; } = default!;
    public string? Output { get; set; } = default!;

    public List<MetaProperty>? OperableProperties { get; set; } = new List<MetaProperty>();
}