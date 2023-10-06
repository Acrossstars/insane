﻿var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true);
var configuration = configurationBuilder.Build();

var definition = configuration.Get<DomainDefinition>(); 

var domainBuilder = new DomainBuilder(
    configuration,
    metadataDir,
    definition);

domainBuilder.BuildEntities();

domainBuilder.BuildEvents();

domainBuilder.BuildUseCases();