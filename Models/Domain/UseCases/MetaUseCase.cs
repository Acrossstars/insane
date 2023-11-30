using Core.Metadatas;

namespace Core.Domain.UseCases;

public class MetaUseCase
{
    public MetaUseCase() { }
    
    public MetaUseCase(
        string domainEntityName,
        string name,
        RequestType requestType,
        HttpMethodType httpMethodType,
        MetaUseCaseContext metaUseCaseContext,
        List<string> steps,
        bool hasRestEndpoint = true,
        bool hasGrpcEndpoint = false,
        bool returnList = false)
    {
        DomainEntityName = domainEntityName;
        //ManyEntities = DomainEntityName.Pluralize();
        Name = name;
        RequestType = requestType;
        HttpMethodType = httpMethodType;
        Request = Name + "Request";
        RequestHandler = Name + "RequestHandler";

        InputDto = Name + "Dto";

        QueryReturnTypeDto = returnList ? $"List<{Name}Dto>" : Name + "Dto";

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

        UseCaseContext = metaUseCaseContext;
        UseCaseSteps = steps;
    }

    public static MetaUseCase Create()
    {
        var newMetaUseCase = new MetaUseCase();



        return newMetaUseCase;
    }

    public string DomainEntityName { get; set; } = default!;
    public string ManyEntities { get; set; } = default!;
    public string Name { get; set; } = default!;
    public HttpMethodType HttpMethodType { get; set; }
    public RequestType RequestType { get; set; }
    public string Request { get; set; } = default!;
    public string RequestHandler { get; set; } = default!;
    public bool HasRestEndpoint { get; set; }
    public string RestEndpoint { get; set; } = default!;
    public bool HasGrpcEndpoint { get; set; }
    public string GrpcEndpoint { get; set; } = default!;
    public string InputDto { get; set; } = default!;
    public string QueryReturnTypeDto { get; set; } = default!;
    public DtoMetadata DtoMetadata { get; set; }
    public RestEndpointMetadata RestEndpointMetadata { get; set; }
    public List<MetaProperty> ManagedProperties { get; set; } = new List<MetaProperty>();
    public MetaUseCaseContext UseCaseContext { get; set; }
    //public List<MetaUseCaseStep> UseCaseSteps { get; set; }
    public List<string> UseCaseSteps { get; set; }
    public string UseCaseNamespace { get; set; }
}