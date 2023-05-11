namespace Models;

//public record TypeName(string Type, string Name);
//public record Property(string Modificator, string Type, string Name, string[] Accessors);

public class TypeName
{
    string Type { get; set; } = default!;
    string Name { get; set; } = default!;
}

public class Property
{
    string Modificator { get; set; } = default!;
    string Type { get; set; } = default!;
    string Name { get; set; } = default!;
    string[] Accessors { get; set; } = default!;
}