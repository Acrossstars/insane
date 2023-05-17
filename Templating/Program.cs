using Core;
using Core.Domain;
using Core.Domain.Enums;
using Core.Models;
using Core.Models.Common;
using Microsoft.Extensions.Configuration;
using Templating.Infra;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var domainEntity = "Unit";

var featuresToGenerate = new List<string>() { "AddUnit", "UpdateUnit", "DeleteUnit", "GetUnits" };

var useCasesToGenerate = new List<UseCase>()
{
    new UseCase("ИдиНахуйCommand", RequestType.Command),
    new UseCase("AddUnit", RequestType.Command),
    new UseCase("UpdateUnit", RequestType.Command),
    new UseCase("DeleteUnit", RequestType.Command),
    new UseCase("GetUnits", RequestType.Query)
};

var metadataDir = $"{Directory.GetCurrentDirectory()}";

foreach (var useCase in useCasesToGenerate)
{
    var builderContexts = new List<ObjectBuilderContext>();

    List<string> filesToGenerate = new();

    var path = $"\\UseCases\\{useCase.Name}";
    var outputFilePath = $"{configuration["SolutionRootPath"]}\\DraftHuyaft{path}";

    var useCaseNamespace = $"API.UseCases.{domainEntity}s.{useCase.Name}";

    switch (useCase.RequestType)
    {
        case RequestType.Command:

            var commandRequest = new CommandRequestMetadata()
            {
                //no needed perhaps
                FilePath = "",
                Usings = new string[] { },
                Namespace = useCaseNamespace,
                ClassName = useCase.Request,
                Properties = new List<Property>()
                {
                    new Property("public","string","ActionId", new string[]{ "get", "set" }),
                    new Property("public","string","SessionId", new string[]{ "get", "set" }),
                    new Property("public","string","DecisionId", new string[]{ "get", "set" }),
                },
                Constructor = new List<TypeName>()
                {
                    new TypeName("string", "actionId"),
                    new TypeName("string", "sessionId"),
                    new TypeName("string", "decisionId"),
                },
                InjectedProperties = new List<InjectedProperty>()
                {
                    new InjectedProperty("ActionId", "actionId"),
                    new InjectedProperty("SessionId", "sessionId"),
                    new InjectedProperty("DecisionId", "decisionId")
                },
            };

            builderContexts.Add(new ObjectBuilderContext()
            {
                FileName = commandRequest.ClassName,
                Model = commandRequest,
                TextTemplateFilePath = GetTextTemplatePath(commandRequest, metadataDir),
                OutputFilePath = outputFilePath
            });

            var commandRequestHandler = new CommandRequestHandlerMetadata()
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
                    new Property("public","string","ActionId", new string[]{ "get", "set" }),
                    new Property("public","string","SessionId", new string[]{ "get", "set" }),
                    new Property("public","string","DecisionId", new string[]{ "get", "set" }),
                },
                BaseConstructor = new string[]
                {
                    "messageBus",
                    "inMemoryBus"
                },
                Constructor = new List<TypeName>()
                {
                    new TypeName("string", "actionId"),
                    new TypeName("string", "sessionId"),
                    new TypeName("string", "decisionId"),
                },
                InjectedInfrastructure = new List<TypeName>()
                {
                    new TypeName("IMessageBus", "messageBus"),
                    new TypeName("IInMemoryBus", "inMemoryBus")
                },
                InjectedRequestClass = new List<TypeName>()
                {
                    new TypeName(useCase.Request, useCase.Request[0].ToString().ToLower()),
                },
                InjectedProperties = new List<InjectedProperty>()
                {
                    new InjectedProperty("ActionId", "actionId"),
                    new InjectedProperty("SessionId", "sessionId"),
                    new InjectedProperty("DecisionId", "decisionId")
                },
            };

            builderContexts.Add(new ObjectBuilderContext()
            {
                FileName = commandRequestHandler.ClassName,
                Model = commandRequestHandler,
                TextTemplateFilePath = GetTextTemplatePath(commandRequestHandler, metadataDir),
                OutputFilePath = outputFilePath
            });

            break;
        case RequestType.Query:
            //TODO: for query handler needed to add using that contains namespace for query request

            var queryRequest = new QueryRequestMetadata()
            {
                //no needed perhaps
                FilePath = "",
                Usings = new string[] { },
                Namespace = useCaseNamespace,
                ClassName = useCase.Request,
                QueryReturnType = $"{useCase.Name}Dto",
                Properties = new List<Property>()
                {
                    new Property("public","string","ActionId", new string[]{ "get", "set" }),
                    new Property("public","string","SessionId", new string[]{ "get", "set" }),
                    new Property("public","string","DecisionId", new string[]{ "get", "set" }),
                },
                Constructor = new List<TypeName>()
                {
                    new TypeName("string", "actionId"),
                    new TypeName("string", "sessionId"),
                    new TypeName("string", "decisionId"),
                },
                InjectedProperties = new List<InjectedProperty>()
                {
                    new InjectedProperty("ActionId", "actionId"),
                    new InjectedProperty("SessionId", "sessionId"),
                    new InjectedProperty("DecisionId", "decisionId")
                },
            };

            builderContexts.Add(new ObjectBuilderContext()
            {
                FileName = queryRequest.ClassName,
                Model = queryRequest,
                TextTemplateFilePath = GetTextTemplatePath(queryRequest, metadataDir),
                OutputFilePath = outputFilePath
            });

            var queryRequestHandler = new QueryRequestHandlerMetadata()
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
                    new Property("public","string","ActionId", new string[]{ "get", "set" }),
                    new Property("public","string","SessionId", new string[]{ "get", "set" }),
                    new Property("public","string","DecisionId", new string[]{ "get", "set" }),
                },
                Constructor = new List<TypeName>()
                {
                    new TypeName("string", "actionId"),
                    new TypeName("string", "sessionId"),
                    new TypeName("string", "decisionId"),
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
                    new TypeName(useCase.Request, useCase.Request[0].ToString().ToLower()),
                },
                InjectedProperties = new List<InjectedProperty>()
                {
                    new InjectedProperty("ActionId", "actionId"),
                    new InjectedProperty("SessionId", "sessionId"),
                    new InjectedProperty("DecisionId", "decisionId")
                },
            };

            builderContexts.Add(new ObjectBuilderContext()
            {
                FileName = queryRequestHandler.ClassName,
                Model = queryRequestHandler,
                TextTemplateFilePath = GetTextTemplatePath(queryRequestHandler, metadataDir),
                OutputFilePath = outputFilePath
            });
            break;
    }

    if (useCase.HasRestEndpoint)
    {

        var restEndpoint = new RestEndpointMetadata()
        {
            ClassName = useCase.RestEndpoint,
            Usings = new string[]
                {

                },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
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

        builderContexts.Add(new ObjectBuilderContext()
        {
            FileName = restEndpoint.ClassName,
            Model = restEndpoint,
            TextTemplateFilePath = GetTextTemplatePath(restEndpoint, metadataDir),
            OutputFilePath = outputFilePath
        });

    }

    if (useCase.HasGrpcEndpoint)
    {



    }

    foreach (var builderMetadata in builderContexts)
    {
        var builder = new FileBuilder(configuration);
        builder.Build(builderMetadata);
    }
}

static void HzMethod()
{

}

static string GetTextTemplatePath(BaseMetadata item, string metadataDir) => item.Type switch
{
    _ => $"{metadataDir}\\TextTemplates\\{item.Type}TextTemplate.txt",
};

