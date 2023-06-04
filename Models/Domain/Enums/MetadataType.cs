namespace Core.Domain.Enums;

public enum MetadataType
{
    Entity = 0,
    Aggregate = 1,
    ValueObject = 3,
    DomainEvent = 4,
    DomainEventHandler = 5,
    CommandRequest = 6,
    CommandRequestHandler = 7,
    QueryRequest = 8,
    QueryRequestHandler = 9,
    RestEndpoint = 10,
    GrpcEndpoint = 11,
    Dto = 12,
}