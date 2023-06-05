using Core.Domain.Common;
using Core.Domain.Interfaces;

namespace Core.Domain;

public class DomainEventContext : IDataContext
{
    public string? DomainEntityName { get; set; }
    public List<MetaProperty>? OperableProperties { get; set; }
}