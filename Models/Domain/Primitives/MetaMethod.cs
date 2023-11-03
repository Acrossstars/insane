namespace Core.Domain.Common;

public class MetaMethod
{
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<TypeName> Parameters { get; set; } = new List<TypeName>();
    public string Logic { get; set; } = string.Empty;
}