namespace Core.Domain;

/// <summary>
/// 
/// </summary>
public class MetaUseCaseContext
{
    /// <summary>
    /// 
    /// </summary>
    public MetaUseCaseContext()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public string? DomainEntityName { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public List<MetaProperty>? OperableProperties { get; set; }
}