using Core.Domain;
using Core.Domain.Enums;
using Core.Models;
using Core.Models.Common;
using Microsoft.Extensions.Configuration;
using Templating.Services;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var _domainEntity = "SomeEntity";

var dtosPath = configuration["SolutionRootPath"] + configuration["DtoPath"];


var domainDefinition = new DomainDefinition()
{
    Entities = new List<EntityMetadata>()
    {
        new EntityMetadata()
        {
            ClassName = "WholeSome",
            Properties = new List<Property>()
            {
                new Property("public", "string", "Id", GetAccessorsArray()),
                new Property("public", "string", "Name", GetAccessorsArray()),
                new Property("public", "string", "Description", GetAccessorsArray()),
            }
        }
    },
    Aggregates = new List<string>()
    {
        "SomeEntity",
    },

    DomainEvents = new List<string>()
    {
        "SomeEntityUpdated",
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
    UseCases = new List<UseCase>()
    {
        #region MyRegion
        //new UseCase("AddUnit", RequestType.Command, HttpMethodType.Post),
        //new UseCase("UpdateUnit", RequestType.Command, HttpMethodType.Put),
        //new UseCase("DeleteUnit", RequestType.Command, HttpMethodType.Delete),
        //new UseCase("GetUnits", RequestType.Query, HttpMethodType.Get)
        //new UseCase($"Request{_domainEntity}", RequestType.Command, HttpMethodType.Post),
        //new UseCase($"Accept{_domainEntity}", RequestType.Command, HttpMethodType.Put),
        //new UseCase($"Declined{_domainEntity}", RequestType.Command, HttpMethodType.Delete),
        //new UseCase($"Get{_domainEntity}s", RequestType.Query, HttpMethodType.Get)
        //new UseCase($"Add{_domainEntity}", RequestType.Command, HttpMethodType.Post),
        //new UseCase($"Delete{_domainEntity}", RequestType.Command, HttpMethodType.Delete),
        //new UseCase($"Get{_domainEntity}", RequestType.Query, HttpMethodType.Get)
        //new UseCase($"Login{domainEntity}", RequestType.Command, HttpMethodType.Post),
        //new UseCase($"Register{domainEntity}", RequestType.Command, HttpMethodType.Post),
        //new UseCase($"Update{domainEntity}", RequestType.Command, HttpMethodType.Put), 
        #endregion
    }
};

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var newUseCase = new UseCase($"{_domainEntity}", $"Update{_domainEntity}", RequestType.Command, HttpMethodType.Put)
{

};

domainDefinition.UseCases.Add(newUseCase);

var domainBuilder = new DomainBuilder(configuration, dtosPath, metadataDir, domainDefinition);

domainBuilder.BuildEvents();

foreach (var useCase in domainDefinition.UseCases)
{
    var userCasesBuilder = new UserCasesBuilder(configuration, domainDefinition, useCase);

    userCasesBuilder.GenerateUseCase(configuration, _domainEntity, dtosPath, metadataDir);
}




static string[] GetAccessorsArray()
{
    return new string[] { "get", "set" };
}