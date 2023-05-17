namespace Core.Domain.Enums;

public enum MetadataType
{
    Entity = 0,
    Aggregate = 1,
    ValueObject = 3,
    DomainEvent = 4,
    CommandRequest = 5,
    CommandRequestHandler = 6,
    QueryRequest = 7,
    QueryRequestHandler = 8,
    RestEndpoint = 9,
    GrpcEndpoint = 10,
}