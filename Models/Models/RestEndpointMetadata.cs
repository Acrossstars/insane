namespace Core.Models;

public class RestEndpointMetadata : YetAnotherBaseMetadata
{
    public RestEndpointMetadata()
    {
        Type = MetadataType.RestEndpoint;
    }

    public string? InputType { get; set; }
    public string? Tags { get; set; }
    public string? Route { get; set; }
    public string MethodReturnType { get; set; }
    public string HttpMethod { get; set; }
    public string InMemoryBusMethod { get; set; }
    public List<InjectedProperty>? InputProperties { get; set; } = new List<InjectedProperty>();
}