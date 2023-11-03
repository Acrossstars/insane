namespace Core.Metadatas.Repositories;

public class RepositoryMetadata : BaseMetadata
{
    public RepositoryMetadata()
    {
    }

    public RepositoryMetadata(RepositoryMetadata repositoryMetadata)
    {
        if (repositoryMetadata == null) 
            throw new ArgumentNullException(nameof(repositoryMetadata));

        AggregateEntity = repositoryMetadata.AggregateEntity;
        Methods = repositoryMetadata.Methods;
        InterfaceName = repositoryMetadata.InterfaceName;
        DbContext = repositoryMetadata.DbContext;
    }

    public string AggregateEntity { get; set; } = string.Empty;
    public List<MetaMethod> Methods { get; set; } = new List<MetaMethod>();
    public string InterfaceName { get; set; } = string.Empty;
    public string DbContext { get; set; } = string.Empty;
}