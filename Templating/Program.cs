using Core.Analyzing;
using Core.Generation;
using Microsoft.Build.Locator;
using Microsoft.Extensions.Configuration;

MSBuildLocator.RegisterDefaults();

//var projectsToSearch = new List<string> { "UniqMerch.SmartContracts", "UniqMerch.API" };
var projectsToSearch = new List<string> { "DataService.API", "Decider.API", "Behavior.API" };


var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("Configurations/Repositories/repository.json", false, true);

var configuration = configurationBuilder.Build();

//var analyzer = new Analyzer(
//    $"{configuration["SolutionRootPath"]}\\{configuration["SolutionFileName"]}");

//await analyzer.ExtractUsings(projectsToSearch);

var definition = configuration.Get<DomainDefinition>();
var generationDesign = new GenerationDesign();
var pathService = new PathNameSpacesService(configuration, generationDesign);

var domainBuilder = new DomainBuilder(
    configuration,
    metadataDir,
    pathService,
    generationDesign,
    definition);


domainBuilder.BuildPostgreSqlRepository();

domainBuilder.BuildEntities();


domainBuilder.BuildEvents();

domainBuilder.BuildUseCases();