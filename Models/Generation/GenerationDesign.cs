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

    }

    public GenerationStyleType GenerationStyle { get; set; }
    public string? DomainLayerNamespaceRoot { get; set; }
    public string EventsFolderName { get; set; }
    public string EventHandlersFolderName { get; set; }
    public string UseCasesFolderName { get; set; }
}