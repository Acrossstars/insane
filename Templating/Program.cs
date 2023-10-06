using Core.Generation;
using Microsoft.Extensions.Configuration;

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true);
var configuration = configurationBuilder.Build();

var definition = configuration.Get<DomainDefinition>();

var pathService = new PathService(configuration);

var domainBuilder = new DomainBuilder(
    configuration,
    metadataDir,
    pathService,
    definition);

domainBuilder.BuildEntities();

domainBuilder.BuildEvents();

domainBuilder.BuildUseCases();