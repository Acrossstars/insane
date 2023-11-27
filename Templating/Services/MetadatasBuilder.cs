using Core.Domain;
using Core.Domain.Common;
using Core.Domain.Enums;
using Core.Domain.UseCases;
using Core.Extensions;
using Core.Generation;
using Core.Metadatas;
using Core.Metadatas.Commands;
using Core.Metadatas.Queries;
using Core.Metadatas.Repositories;

namespace Templating.Services;

internal class MetadatasBuilder
{
    private readonly GenerationDesign _generationDesign;
    private readonly DomainDefinition _domainDefinition;

    public MetadatasBuilder(
        GenerationDesign generationDesign,
        DomainDefinition domainDefinition
        )
    {
        _generationDesign = generationDesign;
        _domainDefinition = domainDefinition;
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
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            OperationType = GetCrudOperationFromHttpMethod(useCase.HttpMethodType),
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

        //*****************************************************************************

        AwesomeHelper.InjectRepositoryIntoMetadata(useCase.UseCaseContext, metadata);

        if (_domainDefinition.RepositoryInterfaces != null && _domainDefinition.RepositoryInterfaces.Count != 0)
        {
            var repositoryInterfaceNamespace = _domainDefinition.RepositoryInterfaces!.FirstOrDefault()!.Namespace;

            metadata.Usings = new string[] { repositoryInterfaceNamespace };
        }
        
        //*****************************************************************************

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
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto",
            ClassName = useCase.RequestHandler,
            OperationType = GetCrudOperationFromHttpMethod(useCase.HttpMethodType),
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

        //*****************************************************************************

        AwesomeHelper.InjectRepositoryIntoMetadata(useCase.UseCaseContext, metadata);

        if (_domainDefinition.RepositoryInterfaces != null && _domainDefinition.RepositoryInterfaces.Count != 0)
        {

            var repositoryInterfaceNamespace = _domainDefinition.RepositoryInterfaces!.FirstOrDefault()!.Namespace;

            metadata.Usings = new string[] { repositoryInterfaceNamespace! };

        }

        //*****************************************************************************

        return metadata;
    }

    private CrudType GetCrudOperationFromHttpMethod(HttpMethodType httpMethodType)
    {
        switch (httpMethodType)
        {
            case HttpMethodType.Post:
                return CrudType.Create;

            case HttpMethodType.Put:
                return CrudType.Update;

            case HttpMethodType.Delete:
                return CrudType.Delete;

            case HttpMethodType.Get:
                return CrudType.Read;

            default:
                break;
        }

        return CrudType.None;
    }
}