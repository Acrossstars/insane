using Core.Generation;
using Microsoft.Extensions.Configuration;

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true);
var configuration = configurationBuilder.Build();

var definition = configuration.Get<DomainDefinition>();
var generationDesign = new GenerationDesign();
var pathService = new PathNameSpacesService(configuration, generationDesign);

var domainBuilder = new DomainBuilder(
    configuration,
    metadataDir,
    pathService,
    generationDesign,
    definition);

domainBuilder.BuildEntities();

domainBuilder.BuildEvents();

domainBuilder.BuildUseCases();