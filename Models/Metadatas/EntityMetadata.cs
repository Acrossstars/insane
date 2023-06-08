namespace Core.Metadatas;

public class EntityMetadata : BaseMetadata, IDataContext, IMetaProperties //TODO: разобраться с интерфейсами и как они используются
{
    public EntityMetadata()
    {
        Type = MetadataType.Entity;
    }

    public string? DomainEntityName { get; set; }
    public List<MetaProperty>? OperableProperties { get; set; }
    public List<TypeName>? CreateParameters { get; set; }
}