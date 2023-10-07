using System.Text.Json.Serialization;

namespace Core.Domain.Common;

public class BaseMetadata
{
    public List<string>? BaseEntities { get; set; }
    public string? ClassName { get; set; }
    public List<MetaProperty>? Properties { get; set; }

    public string? FilePath { get; set; }
    public MetadataType Type { get; set; }
    public string[]? Usings { get; set; }
    public string? Namespace { get; set; }

    public List<TypeName>? PrivateFields { get; set; }
    public List<TypeName>? Constructor { get; set; }
    public string[]? BaseConstructor { get; set; }
    public List<InjectedProperty>? InjectedProperties { get; set; }
    
}