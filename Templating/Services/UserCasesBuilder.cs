using Core.Domain.Enums;
using Core.Domain;
using Core.Models;
using Core;
using Microsoft.Extensions.Configuration;
using Templating.Infra;
using Core.Models.Common;
using Core.Extensions;
using Templating.Features;

namespace Templating.Services;

public class UserCasesBuilder
{
    public UserCasesBuilder()
    {

    }

    //TODO: Rename Namespace to Match Folder Structure
    public static void GenerateUseCases(IConfigurationRoot configuration, string domainEntity, string dtosPath, List<UseCase> useCasesToGenerate, string metadataDir)
    {
        foreach (var useCase in useCasesToGenerate)
        {
            var builderContexts = new List<ObjectBuilderContext>();

            List<string> filesToGenerate = new();

            var useCasesFolderName = $"UseCases";

            var manyEntities = $"{domainEntity}s";

            var path = $"\\{useCasesFolderName}\\{manyEntities}\\{useCase.Name}";

            string? solutionRoot = configuration["SolutionRootPath"];

            string? apiRoot = configuration["ApiRootPath"];

            var outputFilePath = $"{solutionRoot}{apiRoot}\\{path}";

            var useCaseNamespace = $"UseCases.{manyEntities}.{useCase.Name}";

            switch (useCase.RequestType)
            {
                case RequestType.Command:

                    var commandRequest = GetCommandRequest(useCase, useCaseNamespace);

                    BuildTools.AppendToBuild(metadataDir, builderContexts, outputFilePath, commandRequest, commandRequest.ClassName);

                    var commandRequestHandler = GetCommandRequestHandler(useCase, useCaseNamespace);

                    BuildTools.AppendToBuild(metadataDir, builderContexts, outputFilePath, commandRequestHandler, commandRequestHandler.ClassName);

                    break;
                case RequestType.Query:
                    //TODO: for query handler needed to add using that contains namespace for query request
                    var queryRequest = CreateQueryRequestMetadata(useCase, useCaseNamespace);

                    BuildTools.AppendToBuild(metadataDir, builderContexts, outputFilePath, queryRequest, queryRequest.ClassName);

                    var queryRequestHandler = CreateQueryRequestHandlerMetadata(useCase, useCaseNamespace);

                    BuildTools.AppendToBuild(metadataDir, builderContexts, outputFilePath, queryRequestHandler, queryRequestHandler.ClassName);
                    break;
            }

            if (useCase.HasRestEndpoint)
            {
                RestEndpointMetadata restEndpoint = CreateRestEndpointMetadata(domainEntity, useCase, useCaseNamespace);

                BuildTools.AppendToBuild(metadataDir, builderContexts, outputFilePath, restEndpoint, restEndpoint.ClassName);

            }

            if (useCase.HasGrpcEndpoint)
            {



            }

            var dto = GetDto(useCase, useCaseNamespace);
            var dtoPath = dtosPath + $"\\{domainEntity}s";
            BuildTools.AppendToBuild(metadataDir, builderContexts, dtoPath, dto, dto.ClassName);

            foreach (var builderMetadata in builderContexts)
            {
                var builder = new FileBuilder(configuration);
                builder.Build(builderMetadata);
            }
        }
    }

    static string GetMethodReturnType(UseCase useCase)
    {
        return useCase.RequestType switch
        {
            RequestType.Query => useCase.QueryReturnTypeDto,
            RequestType.Command => "CommandResult",
        };
    }

    static RestEndpointMetadata CreateRestEndpointMetadata(string domainEntity, UseCase useCase, string useCaseNamespace)
    {
        return new RestEndpointMetadata()
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
            },
            InjectedProperties = new List<InjectedProperty>()
            {

            },
        };
    }

    static QueryRequestHandlerMetadata CreateQueryRequestHandlerMetadata(UseCase useCase, string useCaseNamespace)
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

    static QueryRequestMetadata CreateQueryRequestMetadata(UseCase useCase, string useCaseNamespace)
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

    static DtoMetadata GetDto(UseCase useCase, string useCaseNamespace)
    {
        return new DtoMetadata()
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

            },
            Constructor = new List<TypeName>()
            {

            },
            InjectedProperties = new List<InjectedProperty>()
            {

            },
        };
    }

    static CommandRequestHandlerMetadata GetCommandRequestHandler(UseCase useCase, string useCaseNamespace)
    {
        return new CommandRequestHandlerMetadata()
        {
            ClassName = useCase.RequestHandler,
            //no needed perhaps
            FilePath = "",
            Usings = new string[]
                        {

                        },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            Properties = new List<Property>()
            {
                //    new Property("public","string","ActionId", new string[]{ "get", "set" }),
                //    new Property("public","string","SessionId", new string[]{ "get", "set" }),
                //    new Property("public","string","DecisionId", new string[]{ "get", "set" }),
            },
            BaseConstructor = new string[]
                        {
                    "messageBus",
                    "inMemoryBus"
                        },
            Constructor = new List<TypeName>()
            {
                //new TypeName("string", "actionId"),
                //new TypeName("string", "sessionId"),
                //new TypeName("string", "decisionId"),
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
    }

    static CommandRequestMetadata GetCommandRequest(UseCase useCase, string useCaseNamespace)
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

}
