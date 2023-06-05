using Core.Domain.Common;
using Core.Domain.Interfaces;

namespace Core.Domain.UseCases;

/// <summary>
/// 
/// </summary>
public class MetaUseCaseContext : IDataContext
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
    public List<MetaProperty>? OperableProperties { get; set; } = new List<MetaProperty>();
}