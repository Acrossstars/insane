using Core.Domain.Common;
using Core.Domain.Enums;
using Core.Domain.UseCases;
using Core.Extensions;
using Core.Generation;
using Core.Metadatas;
using Core.Metadatas.Commands;
using Core.Metadatas.Queries;

namespace Templating.Services;

internal class MetadatasBuilder
{
    private readonly GenerationDesign _generationDesign;
    private readonly PathNameSpacesService _pathNameSpacesService;

    public MetadatasBuilder(
        GenerationDesign generationDesign,
        PathNameSpacesService pathNameSpacesService
        )
    {
        _generationDesign = generationDesign;
        _pathNameSpacesService = pathNameSpacesService;
    }

    public static DtoMetadata GetDto(MetaUseCase useCase, string useCaseNamespace)
    {
        var metadata = new DtoMetadata()
        {
            ClassName = useCase.RequestType switch
            {
                RequestType.Command => useCase.InputDto,
                RequestType.Query => useCase.QueryReturnTypeDto,
                _ => throw new NotImplementedException()
            },
            Usings = new string[]
            {

            },

            Namespace = useCaseNamespace,
            //Properties = new List<MetaProperty>()
            //{
            //    new MetaProperty("public","int","Id", new string[]{ "get", "set" }),
            //    new MetaProperty("public","string","Name", new string[]{ "get", "set" }),
            //    new MetaProperty("public","string","Description", new string[]{ "get", "set" }),
            //},
        };

        //metadata.InjectedInfrastructure = new List<TypeName>();
        //metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        //нихуя себе!

        useCase.UseCaseContext.OperableProperties!.ForEach(x =>
        {
            //metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

            metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, AwesomeHelper.GetAccessorsArray()));

            //metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        });

        return metadata;
    }

    public RestEndpointMetadata CreateRestEndpointMetadata(string domainEntity, MetaUseCase useCase, string useCaseNamespace)
    {
        var metadata = new RestEndpointMetadata()
        {
            BaseEntities = new List<string>()
            {
                _generationDesign.RestEndpointBaseСlassName!
            },
            ClassName = useCase.RestEndpoint,
            Usings = new string[]
            {

            },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            MethodReturnType = AwesomeHelper.GetMethodReturnType(useCase),
            HttpMethod = useCase.HttpMethodType.ToString(),
            InMemoryBusMethod = useCase.RequestType.ToString(),
            InputType = $"{useCase.Name}Dto",
            Tags = $"\"{domainEntity.Pluralize()}\"",
            Route = $"\"{useCase.Name}\"",
            BaseConstructor = new string[]
            {
                "authenticatedUserService",
                "inMemoryBus"
            },
            InjectedInfrastructure = new List<TypeName>()
            {
                new TypeName("IAuthenticatedUserService", "authenticatedUserService"),
                new TypeName("IInMemoryBus", "inMemoryBus")
            },
            InjectedRequestClass = new List<TypeName>()
            {
                new TypeName(useCase.Request, useCase.Request.FirstLetterToLower()),
            }
        };

        useCase.DtoMetadata.Properties!.ForEach(x =>
        {
            metadata.InputProperties!.Add(new InjectedProperty(x.Name.FirstLetterToLower(), x.Name));
        });

        return metadata;
    }

    public CommandRequestMetadata CreateMetaCommandRequest(MetaUseCase useCase, string useCaseNamespace)
    {
        CommandRequestMetadata metadata = new CommandRequestMetadata()
        {
            //no needed perhaps

            FilePath = "",
            Usings = Array.Empty<string>(),

            BaseEntities = new List<string>()
            {
                _generationDesign.CommandRequestBasePattern!
            },

            Namespace = useCaseNamespace,
            ClassName = useCase.Request,
        };

        //string.Format(_generationDesign.CommandRequestBasePattern!, request, returntype);

        metadata.InjectedInfrastructure = new List<TypeName>();
        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        AwesomeHelper.FillOperablePropertiesFromMetadata(useCase.UseCaseContext, metadata);

        return metadata;
    }



    #region CommandRequestHandler

    public CommandRequestHandlerMetadata CreateCommandRequestHandlerMetadata(MetaUseCase useCase, string useCaseNamespace)
    {
        var metadata = new CommandRequestHandlerMetadata()
        {
            BaseEntities = new List<string>()
            {
                string.Format(_generationDesign.CommandRequestHandlerBasePattern!, useCase.Request, "CommandResult")
            },
            ClassName = useCase.RequestHandler,
            //no needed perhaps
            FilePath = "",
            Usings = Array.Empty<string>(),
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,

            PrivateFields = new List<TypeName>()
            {
                new TypeName($"private readonly I{useCase.DomainEntityName}Repository", $"_{useCase.DomainEntityName.FirstLetterToLower()}Repository")
            },

            BaseConstructor = new string[]
            {
                "messageBus",
                "inMemoryBus"
            },
            InjectedInfrastructure = new List<TypeName>()
            {
                new TypeName("IMessageBus", "messageBus"),
                new TypeName("IInMemoryBus", "inMemoryBus"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                new InjectedProperty($"I{useCase.DomainEntityName}Repository", $"{useCase.DomainEntityName.FirstLetterToLower()}Repository")
            },
            InjectedRequestClass = new List<TypeName>()
            {
                new TypeName(useCase.Request, useCase.Request.FirstLetterToLower()),
            },
            Steps = useCase.UseCaseSteps
        };

        metadata.Constructor = new List<TypeName>();
        metadata.PrivateFields = new List<TypeName>();
        metadata.InjectedProperties = new List<InjectedProperty>();
        useCase.UseCaseContext.DomainEntityName = useCase.DomainEntityName;

        AwesomeHelper.InjectRepositoryIntoMetadata(useCase.UseCaseContext, metadata);

        return metadata;
    }

    #endregion

    public QueryRequestMetadata CreateQueryRequestMetadata(MetaUseCase useCase, string useCaseNamespace)
    {
        var metadata = new QueryRequestMetadata()
        {
            //no needed perhaps
            FilePath = "",
            Usings = new string[] { },
            QueryReturnType = $"{useCase.Name}Dto",
            Namespace = useCaseNamespace,
            BaseEntities = new List<string>()
            {
                string.Format(_generationDesign.QueryRequestBasePattern!, $"{useCase.Name}Dto")
            },
            ClassName = useCase.Request,
        };

        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        AwesomeHelper.FillOperablePropertiesFromMetadata(useCase.UseCaseContext, metadata);

        return metadata;
    }

    public QueryRequestHandlerMetadata CreateQueryRequestHandlerMetadata(MetaUseCase useCase, string useCaseNamespace)
    {

        var metadata = new QueryRequestHandlerMetadata()
        {
            Usings = Array.Empty<string>(),
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto",
            ClassName = useCase.RequestHandler,
            BaseEntities = new List<string>()
            {
                string.Format(_generationDesign.QueryRequestHandlerBasePattern!, useCase.Request, $"{useCase.Name}Dto")
            },
            InjectedInfrastructure = new List<TypeName>()
            {
                new TypeName("IMapper", "mapper")
            },
            BaseConstructor = new string[]
            {
                "mapper"
            },
            InjectedRequestClass = new List<TypeName>()
            {
                new TypeName(useCase.Request, "request"),
            },
            Steps = useCase.UseCaseSteps
        };


        metadata.Constructor = new List<TypeName>();
        metadata.PrivateFields = new List<TypeName>();
        metadata.InjectedProperties = new List<InjectedProperty>();
        useCase.UseCaseContext.DomainEntityName = useCase.DomainEntityName;

        AwesomeHelper.InjectRepositoryIntoMetadata(useCase.UseCaseContext, metadata);

        return metadata;
    }
}