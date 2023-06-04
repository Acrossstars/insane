namespace Core.Models.Common;

//public record TypeName(string Type, string Name);
//public record Property(string Modificator, string Type, string Name, string[] Accessors);

public class TypeName
{
    public TypeName(string type, string name)
    {
        Type = type;
        Name = name;
    }

    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
}

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

    public static MetaProperty PublicString(string name)
        => new MetaProperty("public", "string", name, new[] { "get", "set" });
}

public class InjectedProperty
{
    public InjectedProperty(string destination, string source)
    {
        Destination = destination;
        Source = source;
    }

    public string Destination { get; set; } = default!;
    public string Source { get; set; } = default!;
}