namespace Core.Domain.Common;

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
