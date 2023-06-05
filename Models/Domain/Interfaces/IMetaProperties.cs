namespace Core.Domain.Interfaces;

public interface IMetaProperties
{
    /// <summary>
    /// Заменить на конструктор
    /// </summary>
    //public List<TypeName>? InjectedInfrastructure { get; set; }
    public List<TypeName>? Constructor { get; set; }

    public List<InjectedProperty>? InjectedProperties { get; set; }
    public List<MetaProperty>? Properties { get; set; }
}