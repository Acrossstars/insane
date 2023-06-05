namespace Core.Domain.Common;

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