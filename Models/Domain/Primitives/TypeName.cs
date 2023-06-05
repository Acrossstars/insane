namespace Core.Domain.Common;

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
