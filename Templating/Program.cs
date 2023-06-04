﻿var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var _domainEntity = "Test";

var dtosPath = configuration["SolutionRootPath"] + configuration["DtoPath"];

var domainDefinition = new DomainDefinition()
{
    Entities = new List<EntityMetadata>()
    {
        new EntityMetadata()
        {
            ClassName = _domainEntity,
            Properties = new List<MetaProperty>()
            {
                //new MetaProperty("public", "string", "Id", AwesomeHelper.GetAccessorsArray()),
                new MetaProperty("public", "string", "Name", AwesomeHelper.GetAccessorsArray()),
                new MetaProperty("public", "string", "Description", AwesomeHelper.GetAccessorsArray()),
            }
        }
    },
    Aggregates = new List<string>()
    {
        "SomeEntity",
    },

    DomainEvents = new List<string>()
    {
        $"{_domainEntity}Added",
        $"{_domainEntity}Updated",
        $"{_domainEntity}Deleted",
    },
    //Dtos = new List<DtoMetadata>()
    //{
    //    new DtoMetadata()
    //    {
    //        Properties = new List<Property>()
    //        {
    //            new Property("public","int","Id", new string[]{ "get", "set" }),
    //            new Property("public","string","Name", new string[]{ "get", "set" }),
    //            new Property("public","string","Description", new string[]{ "get", "set" }),
    //        }
    //    }
    //},
    UseCases = new List<MetaUseCase>()
    {
    }
};


var addUseCase = new MetaUseCase($"{_domainEntity}", $"Add{_domainEntity}", RequestType.Command, HttpMethodType.Post)
{
    UseCaseContext = new MetaUseCaseContext()
    {
        OperableProperties = new List<MetaProperty>()
        {
            MetaProperty.PublicString("Name"),
            MetaProperty.PublicString("Description")
        }
    }
};
var getUseCase = new MetaUseCase($"{_domainEntity}", $"Get{_domainEntity.Pluralize()}", RequestType.Query, HttpMethodType.Get)
{
    UseCaseContext = new MetaUseCaseContext()
    {
        OperableProperties = new List<MetaProperty>()
        {

        }
    }
};
var updateUseCase = new MetaUseCase($"{_domainEntity}", $"Update{_domainEntity}", RequestType.Command, HttpMethodType.Put)
{
    UseCaseContext = new MetaUseCaseContext()
    {
        OperableProperties = new List<MetaProperty>()
        {
            MetaProperty.PublicString("Id"),
            MetaProperty.PublicString("Name"),
            MetaProperty.PublicString("Description")
        }
    }
};
var deleteUseCase = new MetaUseCase($"{_domainEntity}", $"Delete{_domainEntity}", RequestType.Command, HttpMethodType.Delete)
{
    UseCaseContext = new MetaUseCaseContext()
    {
        OperableProperties = new List<MetaProperty>()
        {
            MetaProperty.PublicString("Id")
        }
    }
};

domainDefinition.UseCases.Add(addUseCase);
domainDefinition.UseCases.Add(getUseCase);
domainDefinition.UseCases.Add(updateUseCase);
domainDefinition.UseCases.Add(deleteUseCase);

var domainBuilder = new DomainBuilder(
    configuration,
    dtosPath,
    metadataDir,
    _domainEntity,
    domainDefinition);

domainBuilder.BuildEntities();
domainBuilder.BuildEvents();

foreach (var useCase in domainDefinition.UseCases)
{
    var userCasesBuilder = new UserCasesBuilder(configuration, domainDefinition, useCase);

    userCasesBuilder.GenerateUseCase(configuration, _domainEntity, dtosPath, metadataDir);
}