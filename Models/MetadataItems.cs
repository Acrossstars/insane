namespace Core;

//public record TypeName(string Type, string Name);
//public record Property(string Modificator, string Type, string Name, string[] Accessors);

public class TypeName
{
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class Property
{
    public string Modificator { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string[] Accessors { get; set; } = default!;
}

public class InjectedProperty
{
    public string Destination { get; set; } = default!;
    public string Source { get; set; } = default!;
}