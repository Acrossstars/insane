using Core.Helpers;

namespace Core.Domain.Common;

public class MetaProperty
{
    public MetaProperty(string modificator, string type, string name, string[] accessors)
    {
        Modificator = modificator;
        Type = type;
        Name = name;
        Accessors = accessors;
    }

    public string Modificator { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string[] Accessors { get; set; } = default!;

    /// <summary>
    /// Fabric method
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static MetaProperty PublicString(string name)
        => new("public", "string", name, AwesomeHelper.GetAccessorsArray());
    
    public static MetaProperty PublicInt(string name)
        => new("public", "int", name, AwesomeHelper.GetAccessorsArray());

    public static MetaProperty PublicDouble(string name)
        => new("public", "double", name, AwesomeHelper.GetAccessorsArray());
}
