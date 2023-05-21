using Core.Domain;
using Core.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Templating.Services;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var domainDefinition = new DomainDefinition()
{
    DomainEvents = new List<string>()
    {
        "UserRegistered",
        "UserUpdated"
    }
};

var domainEntity = "User";

var dtosPath = configuration["SolutionRootPath"] + configuration["DtoPath"];

var useCasesToGenerate = new List<UseCase>()
{
    //new UseCase("AddUnit", RequestType.Command, HttpMethodType.Post),
    //new UseCase("UpdateUnit", RequestType.Command, HttpMethodType.Put),
    //new UseCase("DeleteUnit", RequestType.Command, HttpMethodType.Delete),
    //new UseCase("GetUnits", RequestType.Query, HttpMethodType.Get)
    //new UseCase($"Add{_domainEntity}", RequestType.Command, HttpMethodType.Post),
    //new UseCase($"Update{_domainEntity}", RequestType.Command, HttpMethodType.Put),
    //new UseCase($"Delete{_domainEntity}", RequestType.Command, HttpMethodType.Delete),
    //new UseCase($"Get{_domainEntity}", RequestType.Query, HttpMethodType.Get)
    new UseCase($"Login{domainEntity}", RequestType.Command, HttpMethodType.Post),
    new UseCase($"Register{domainEntity}", RequestType.Command, HttpMethodType.Post),
    new UseCase($"Update{domainEntity}", RequestType.Command, HttpMethodType.Put),
};

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var domainBuilder = new DomainBuilder(configuration, dtosPath, metadataDir, domainDefinition);

domainBuilder.BuildEvents();

//UserCasesBuilder.GenerateUseCases(configuration, domainEntity, dtosPath, useCasesToGenerate, metadataDir);