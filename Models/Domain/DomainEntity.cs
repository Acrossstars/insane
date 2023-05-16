namespace Core.Domain;

public class DomainEntity
{
    public DomainEntity()
    {

    }

    public List<string>? IntegrationEvents { get; set; }
    public List<string>? DomainEvents { get; set; }
    public List<string>? UseCases { get; set; }
    public List<string>? Dtos { get; set; }
}