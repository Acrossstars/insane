using Core.Domain.Common;

namespace Core.Domain.Interfaces;

public interface IMetaProperties
{
    public List<TypeName>? PrivateFields { get; set; }
    public List<TypeName>? Constructor { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
    public List<MetaProperty>? Properties { get; set; }
    public List<TypeName>? InjectedInfrastructure { get; set; }
}