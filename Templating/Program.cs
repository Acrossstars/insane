using Core.ConfigurationsModels;
using Core.Generation;

//MSBuildLocator.RegisterDefaults();

//var projectsToSearch = new List<string> { "UniqMerch.SmartContracts", "UniqMerch.API" };
//var projectsToSearch = new List<string> { "DataService.API", "Decider.API", "Behavior.API" };

var projectConfigName = "Scumdoff.AdminPanel";

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"Configurations/Projects/{projectConfigName}.json", false, true)
    ;
    //.AddJsonFile("Configurations/Repositories/repository.json", false, true)

var configuration = configurationBuilder.Build();

var projectConfig =  new ProjectConfig();
configuration.Bind(nameof(ProjectConfig), projectConfig);

//var analyzer = new Analyzer(
//    $"{configuration["SolutionRootPath"]}\\{configuration["SolutionFileName"]}");

//await analyzer.ExtractUsings(projectsToSearch);

var definition = configuration.Get<DomainDefinition>();
var generationDesign = new GenerationDesign();
//var pathService = new PathNameSpacesService(configuration, generationDesign);
var pathService = new PathService(projectConfig);
var namespaceService = new NamespaceService(projectConfig);

var domainBuilder = new DomainBuilder(
    metadataDir,
    generationDesign,
    definition,
    pathService,
    namespaceService);

domainBuilder.BuildEntities();

domainBuilder.BuildEvents();

domainBuilder.BuildPostgreSqlRepository();

domainBuilder.BuildUseCases();