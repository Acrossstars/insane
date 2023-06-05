using Core.Domain.Common;

namespace Core.Domain.Interfaces;

public interface IInjectedInfrastructure
{
    public List<TypeName>? InjectedInfrastructure { get; set; }

    /// <summary>
    /// Change it
    /// </summary>
    public List<TypeName>? InjectedPrivateFields { get; set; }
}