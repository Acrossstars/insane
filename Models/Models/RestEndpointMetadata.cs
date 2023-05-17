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
}