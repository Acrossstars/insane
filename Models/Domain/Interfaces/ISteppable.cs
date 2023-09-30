using Core.Domain.UseCases;

namespace Core.Domain.Interfaces;

/// <summary>
/// SteppenWolf
/// </summary>
public interface ISteppable
{
    //List<MetaUseCaseStep> Steps { get; set; } 
    List<string> Steps { get; set; } 
}