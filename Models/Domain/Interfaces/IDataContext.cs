namespace Core.Domain.Interfaces;

public interface IDataContext
{
    /// <summary>
    /// 
    /// </summary>
    public string? DomainEntityName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<MetaProperty>? OperableProperties { get; set; }
}
