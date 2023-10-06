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

        CommandRequestBasePattern = "IRequest";
        CommandRequestHandlerBaseePattern = "IRequestHandler<{0}{1}>";
        
        QueryRequestBasePattern = "IRequest<{0}>";
        QueryRequestHandlerBaseePattern = "IRequestHandler<{0}{1}>";

    }

    public GenerationStyleType GenerationStyle { get; set; }
    public string? DomainLayerNamespaceRoot { get; set; }
    public string? EntitiesFolderName { get; set; }
    public string? EventsFolderName { get; set; }
    public string? EventHandlersFolderName { get; set; }
    public string? UseCasesFolderName { get; set; }
    public string? EventСlassNamePattern { get; set; }
    public string? EventHandlerСlassNamePattern { get; set; }
    public string? CommandRequestBasePattern { get; set; }
    public string? CommandRequestHandlerBaseePattern { get; set; }
    public string? QueryRequestBasePattern { get; set; }
    public string? QueryRequestHandlerBaseePattern { get; set; }


}