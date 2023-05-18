using Core;
using Core.Domain;
using Core.Domain.Enums;
using Core.Models;
using Core.Models.Common;
using Microsoft.Extensions.Configuration;
using Templating.Infra;
using Templating.Services;

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true);

var configuration = configurationBuilder.Build();

var domainEntity = "UnitProperty";

//var featuresToGenerate = new List<string>() { "AddUnit", "UpdateUnit", "DeleteUnit", "GetUnits" };

var dtosPath = "D:\\appfox\\desert-brawl-sharp\\src\\BuildingBlocks\\Contracts\\Dtos";

//var dtosPath = "D:\\templates\\subject\\BuildingBlocks\\Contracts\\Dtos";

var useCasesToGenerate = new List<UseCase>()
{
    //new UseCase("AddUnit", RequestType.Command, HttpMethodType.Post),
    //new UseCase("UpdateUnit", RequestType.Command, HttpMethodType.Put),
    //new UseCase("DeleteUnit", RequestType.Command, HttpMethodType.Delete),
    //new UseCase("GetUnits", RequestType.Query, HttpMethodType.Get)
    new UseCase("AddUnitProperty", RequestType.Command, HttpMethodType.Post),
    new UseCase("UpdateUnitProperty", RequestType.Command, HttpMethodType.Put),
    new UseCase("DeleteUnitProperty", RequestType.Command, HttpMethodType.Delete),
    new UseCase("GetUnitProperties", RequestType.Query, HttpMethodType.Get)
};

var metadataDir = $"{Directory.GetCurrentDirectory()}";

UserCasesBuilder.GenerateUseCases(configuration, domainEntity, dtosPath, useCasesToGenerate, metadataDir);