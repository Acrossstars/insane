namespace Core.Generation.Interface;
public interface IEventsPathInterface
{
    public string? EventsOutputFilePath { get; set; }
    public string? EventHandlersOutputFilePath { get; set; }
}