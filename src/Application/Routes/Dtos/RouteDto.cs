namespace Application.Routes.Dtos;

public sealed record RouteDto
{
    public required Guid Id { get; init; }
    
    public required string Origin { get; init; }
    
    public required string Destination { get; init; }

    public required DateTime OriginDateTime { get; init; }
    
    public required DateTime DestinationDateTime { get; init; }
    
    public required decimal Price { get; init; }
    
    public required DateTime TimeLimit { get; init; }
}