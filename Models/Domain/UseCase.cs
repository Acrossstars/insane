namespace Core.Domain;

public class UseCase
{
    public UseCase(
        string name,
        RequestType requestType,
        HttpMethodType httpMethodType,
        bool hasRestEndpoint = true,
        bool hasGrpcEndpoint = false)
    {
        Name = name;
        RequestType = requestType;
        HttpMethodType = httpMethodType;
        Request = Name + "Request";
        RequestHandler = Name + "RequestHandler";

        InputDto = Name + "Dto";

        QueryReturnTypeDto = Name + "Dto";

        HasRestEndpoint = hasRestEndpoint;

        if (HasRestEndpoint)
        {
            RestEndpoint = Name + "RestEndpoint";
        }

        HasGrpcEndpoint = hasGrpcEndpoint;

        if (HasGrpcEndpoint)
        {
            GrpcEndpoint = Name + "GrpcEndpoint";
        }
    }

    public string Name { get; set; } = default!;
    public HttpMethodType HttpMethodType { get; }
    public RequestType RequestType { get; }
    public string Request { get; set; } = default!;
    public string RequestHandler { get; set; } = default!;
    public bool HasRestEndpoint { get; set; }
    public string RestEndpoint { get; set; } = default!;
    public bool HasGrpcEndpoint { get; set; }
    public string GrpcEndpoint { get; set; } = default!;
    public string InputDto { get; set; } = default!;
    public string QueryReturnTypeDto { get; set; } = default!;
}
