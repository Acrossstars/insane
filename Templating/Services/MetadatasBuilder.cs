using Core.Extensions;

namespace Templating.Services;

internal class MetadatasBuilder
{
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

    public static RestEndpointMetadata CreateRestEndpointMetadata(string domainEntity, MetaUseCase useCase, string useCaseNamespace)
    {
        var metadata = new RestEndpointMetadata()
        {
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
            Tags = $"\"{domainEntity}s\"",
            Route = $"\"{useCase.Name}\"",
            Properties = new List<MetaProperty>()
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

        //metadata.InjectedInfrastructure = new List<TypeName>();
        //metadata.Constructor = new List<TypeName>();
        //metadata.Properties = new List<MetaProperty>();
        //metadata.InjectedProperties = new List<InjectedProperty>();

        ////нихуя себе!

        //useCase.UseCaseContext.OperableProperties!.ForEach(x =>
        //{
        //    metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

        //    metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, AwesomeHelper.GetAccessorsArray()));

        //    metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        //});

        useCase.DtoMetadata.Properties.ForEach(x =>
        {
            metadata.InputProperties.Add(new InjectedProperty(x.Name.FirstLetterToLower(), x.Name));
        });

        return metadata;
    }

    public static CommandRequestMetadata CreateMetaCommandRequest(MetaUseCase useCase, string useCaseNamespace)
    {
        CommandRequestMetadata metadata = new CommandRequestMetadata()
        {
            //no needed perhaps
            FilePath = "",
            Usings = new string[] { },
            Namespace = useCaseNamespace,
            ClassName = useCase.Request,
        };

        metadata.InjectedInfrastructure = new List<TypeName>();
        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        //нихуя себе!

        useCase.UseCaseContext.OperableProperties!.ForEach(x =>
        {
            metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

            metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, AwesomeHelper.GetAccessorsArray()));

            metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        });

        return metadata;
    }



    #region CommandRequestHandler

    public static CommandRequestHandlerMetadata GetCommandRequestHandler(MetaUseCase useCase, string useCaseNamespace)
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
                }
        };





        return metadata;
    }

    #endregion

    public static QueryRequestMetadata CreateQueryRequestMetadata(MetaUseCase useCase, string useCaseNamespace)
    {
        QueryRequestMetadata metadata = new QueryRequestMetadata()
        {
            //no needed perhaps
            FilePath = "",
            Usings = new string[] { },
            Namespace = useCaseNamespace,
            ClassName = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto"
        };

        metadata.InjectedInfrastructure = new List<TypeName>();
        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        //нихуя себе!

        useCase.UseCaseContext.OperableProperties!.ForEach(x =>
        {
            metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

            metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, AwesomeHelper.GetAccessorsArray()));

            metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        });

        return metadata;
    }

    public static QueryRequestHandlerMetadata CreateQueryRequestHandlerMetadata(MetaUseCase useCase, string useCaseNamespace)
    {
        QueryRequestHandlerMetadata metadata = new QueryRequestHandlerMetadata()
        {
            ClassName = useCase.RequestHandler,
            Usings = new string[]
                                {

                                },
            Namespace = useCaseNamespace,
            RequestType = useCase.Request,
            QueryReturnType = $"{useCase.Name}Dto",
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
                }
        };

        return metadata;
    }

    /// <summary>
    /// For MetaRequest
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="metadata"></param>
    private static void SetupProperties(MetaUseCase useCase, CommandRequestMetadata metadata)
    {
        metadata.InjectedInfrastructure = new List<TypeName>();
        metadata.Constructor = new List<TypeName>();
        metadata.Properties = new List<MetaProperty>();
        metadata.InjectedProperties = new List<InjectedProperty>();

        //нихуя себе!

        useCase.UseCaseContext.OperableProperties!.ForEach(x =>
        {
            metadata.InjectedInfrastructure.Add(new TypeName(x.Type, x.Name.FirstLetterToLower()));

            metadata.Properties.Add(new MetaProperty(x.Modificator, x.Type, x.Name, AwesomeHelper.GetAccessorsArray()));

            metadata.InjectedProperties.Add(new InjectedProperty(x.Name, x.Name.FirstLetterToLower()));
        });
    }
}