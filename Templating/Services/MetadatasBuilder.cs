using Core.Domain;
using Core.Domain.Enums;
using Core.Extensions;
using Core.Models;
using Core.Models.Common;

namespace Templating.Services;

internal class MetadatasBuilder
{
    public static DtoMetadata GetDto(UseCase useCase, string useCaseNamespace)
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
            Properties = new List<Property>()
            {
                new Property("public","int","Id", new string[]{ "get", "set" }),
                new Property("public","string","Name", new string[]{ "get", "set" }),
                new Property("public","string","Description", new string[]{ "get", "set" }),
            },
            Constructor = new List<TypeName>()
            {

            },
            InjectedProperties = new List<InjectedProperty>()
            {

            },
        };

        return metadata;
    }

    public static RestEndpointMetadata CreateRestEndpointMetadata(string domainEntity, UseCase useCase, string useCaseNamespace)
    {
        var metadata = new RestEndpointMetadata()
        {
            ClassName = useCase.RestEndpoint,
            Usings = new string[]
                        {

                        },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            MethodReturnType = GetMethodReturnType(useCase),
            HttpMethod = useCase.HttpMethodType.ToString(),
            InMemoryBusMethod = useCase.RequestType.ToString(),
            InputType = $"{useCase.Name}Dto",
            Tags = $"\"{domainEntity}s\"",
            Route = $"\"{useCase.Name}\"",
            Properties = new List<Property>()
            {

            },
            Constructor = new List<TypeName>()
            {

            },
            InjectedInfrastructure = new List<TypeName>()
            {
                new TypeName("IAuthenticatedUserService", "authenticatedUserService"),
                new TypeName("IInMemoryBus", "inMemoryBus")
            },
            BaseConstructor = new string[]
                    {
                "authenticatedUserService",
                "inMemoryBus"
                    },
            InjectedRequestClass = new List<TypeName>()
            {
                new TypeName(useCase.Request, useCase.Request[0].ToString().ToLower()),
            }
        };

        useCase.DtoMetadata.Properties.ForEach(x =>
        {
            metadata.InputProperties.Add(new InjectedProperty(x.Name.FirstLetterToLower(), x.Name));
        });

        return metadata;
    }

    public static CommandRequestHandlerMetadata GetCommandRequestHandler(UseCase useCase, string useCaseNamespace)
    {
        var metadata = new CommandRequestHandlerMetadata()
        {
            ClassName = useCase.RequestHandler,
            //no needed perhaps
            FilePath = "",
            Usings = new string[]
                                {

                                },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            BaseConstructor = new string[]
                                {
                    "messageBus",
                    "inMemoryBus"
                                },
            InjectedInfrastructure = new List<TypeName>()
                {
                    new TypeName("IMessageBus", "messageBus"),
                    new TypeName("IInMemoryBus", "inMemoryBus")
                },
            InjectedRequestClass = new List<TypeName>()
                {
                    new TypeName(useCase.Request, useCase.Request.FirstLetterToLower()),
                },
            //InjectedProperties = new List<InjectedProperty>()
            //{
            //    new InjectedProperty("ActionId", "actionId"),
            //    new InjectedProperty("SessionId", "sessionId"),
            //    new InjectedProperty("DecisionId", "decisionId")
            //},
        };

        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<Property>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        //нихуя себе!

        metadata.Constructor.Add(new TypeName("string", "actionId"));

        metadata.Properties.Add(new Property("public", "string", "ActionId", GetAccessorsArray()));

        metadata.InjectedProperties.Add(new InjectedProperty("ActionId", "actionId"));



        return metadata;
    }

    private static string[] GetAccessorsArray()
    {
        return new string[] { "get", "set" };
    }

    public static QueryRequestMetadata CreateQueryRequestMetadata(UseCase useCase, string useCaseNamespace)
    {
        return new QueryRequestMetadata()
        {
            //no needed perhaps
            FilePath = "",
            Usings = new string[] { },
            Namespace = useCaseNamespace,
            ClassName = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto",
            Properties = new List<Property>()
            {
                //new Property("public","string","ActionId", new string[]{ "get", "set" }),
                //new Property("public","string","SessionId", new string[]{ "get", "set" }),
                //new Property("public","string","DecisionId", new string[]{ "get", "set" }),
            },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "actionId"),
                //new TypeName("string", "sessionId"),
                //new TypeName("string", "decisionId"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                //new InjectedProperty("ActionId", "actionId"),
                //new InjectedProperty("SessionId", "sessionId"),
                //new InjectedProperty("DecisionId", "decisionId")
            },
        };
    }

    public static QueryRequestHandlerMetadata CreateQueryRequestHandlerMetadata(UseCase useCase, string useCaseNamespace)
    {
        return new QueryRequestHandlerMetadata()
        {
            ClassName = useCase.RequestHandler,
            Usings = new string[]
                        {

                        },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto",
            Properties = new List<Property>()
            {
                //new Property("public","string","ActionId", new string[]{ "get", "set" }),
                //new Property("public","string","SessionId", new string[]{ "get", "set" }),
                //new Property("public","string","DecisionId", new string[]{ "get", "set" }),
            },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "actionId"),
                //new TypeName("string", "sessionId"),
                //new TypeName("string", "decisionId"),
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
                    new TypeName(
                        useCase.Request,
                        "request"
                        //useCase.Request[0].ToString().ToLower()
                        ),
                },
            InjectedProperties = new List<InjectedProperty>()
            {
                //new InjectedProperty("ActionId", "actionId"),
                //new InjectedProperty("SessionId", "sessionId"),
                //new InjectedProperty("DecisionId", "decisionId")
            },
        };
    }





    public static CommandRequestMetadata GetCommandRequest(UseCase useCase, string useCaseNamespace)
    {
        return new CommandRequestMetadata()
        {
            //no needed perhaps
            FilePath = "",
            Usings = new string[] { },
            Namespace = useCaseNamespace,
            ClassName = useCase.Request,
            Properties = new List<Property>()
            {
                //new Property("public","string","ActionId", new string[]{ "get", "set" }),
                //new Property("public","string","SessionId", new string[]{ "get", "set" }),
                //new Property("public","string","DecisionId", new string[]{ "get", "set" }),
            },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "actionId"),
                //new TypeName("string", "sessionId"),
                //new TypeName("string", "decisionId"),
            },
            InjectedProperties = new List<InjectedProperty>()
            {
                //new InjectedProperty("ActionId", "actionId"),
                //new InjectedProperty("SessionId", "sessionId"),
                //new InjectedProperty("DecisionId", "decisionId")
            },
        };
    }

    static string GetMethodReturnType(UseCase useCase)
    {
        return useCase.RequestType switch
        {
            RequestType.Query => useCase.QueryReturnTypeDto,
            RequestType.Command => "CommandResult",
        };
    }
}