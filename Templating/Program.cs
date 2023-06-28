using Core.Domain.Common;
using Core.Domain.UseCases;

var metadataDir = $"{Directory.GetCurrentDirectory()}";

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("Configurations/conf.json", false, true)
    .AddJsonFile("appsettings.json", false, true);
var configuration = configurationBuilder.Build();

var definition = configuration.Get<DomainDefinition>();

var _domainEntity = "Kyk";

var addUseCase = new MetaUseCase($"{_domainEntity}", $"Add{_domainEntity}", RequestType.Command, HttpMethodType.Post)
{
    UseCaseContext = new MetaUseCaseContext()
    {
        OperableProperties = new List<MetaProperty>()
        {
            MetaProperty.PublicString("Id"),
            MetaProperty.PublicDouble("Value"),
            MetaProperty.PublicString("Name"),
            MetaProperty.PublicString("UnitLevel"),
            MetaProperty.PublicString("UnitType"),
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
            MetaProperty.PublicDouble("Value"),
            MetaProperty.PublicString("Name"),
            MetaProperty.PublicString("UnitLevel"),
            MetaProperty.PublicString("UnitType"),
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

definition.UseCases = new List<MetaUseCase>
{
    addUseCase,
    getUseCase,
    updateUseCase,
    deleteUseCase
};

var domainBuilder = new DomainBuilder(
    configuration,
    metadataDir,
    _domainEntity,
    definition);
//domainDefinitionCode);

domainBuilder.BuildEntities();

domainBuilder.BuildEvents();

domainBuilder.BuildUseCases();