using Core.Domain.Enums;
using Core.Generation.Enums;

namespace Core.Generation;

public class GenerationDesign
{
    public GenerationDesign()
    {
        // Костыль, потому что в папке Domain у нас сборка BoundedContexts
        DomainLayerNamespaceRoot = "BoundedContexts";

        EventsFolderName = $"Events";
        EventHandlersFolderName = "EventHandlers";
        UseCasesFolderName = $"UseCases";
        EntitiesFolderName = $"Entities";

        EventСlassNamePattern = "{0}Event";
        EventHandlerСlassNamePattern = "{0}EventHandler";

        EventHandlerBaseСlassNamePattern = "INotificationHandler<{0}>";
        RestEndpointBaseСlassName = "BaseApiController";

        CommandRequestBasePattern = "BaseCommand";
        CommandRequestHandlerBasePattern = "BaseCommandHandler, IRequestHandler <{0}, {1}>";

        QueryRequestBasePattern = "BaseQuery<{0}>";
        QueryRequestHandlerBasePattern = "BaseQueryHandler, IRequestHandler<{0}, {1}>";

        RepositoriesFolderName = $"Repositories";
        InfrastructureDataFolderName = $"Data";


    }

    public GenerationStyleType GenerationStyle { get; set; }
    public string? DomainLayerNamespaceRoot { get; set; }
    public string? EntitiesFolderName { get; set; }
    public string? EventsFolderName { get; set; }
    public string? EventHandlersFolderName { get; set; }
    public string? UseCasesFolderName { get; set; }
    public string? EventСlassNamePattern { get; set; }
    public string? EventHandlerСlassNamePattern { get; set; }
    public string? EventHandlerBaseСlassNamePattern { get; set; }
    public string? RestEndpointBaseСlassName { get; set; }
    public string? CommandRequestBasePattern { get; set; }
    public string? CommandRequestHandlerBasePattern { get; set; }
    public string? QueryRequestBasePattern { get; set; }
    public string? QueryRequestHandlerBasePattern { get; set; }
    public string? RepositoriesFolderName { get; set; }
    public string? InfrastructureDataFolderName { get; set; }

}