using Application.Routes.Dtos;
using MediatR;

namespace Application.Routes.Queries.GetByFilter;

public sealed class GetByFilterQuery : IRequest<RouteSearchResponseDto>
{
    public required string Origin { get; init; }
    public required string Destination { get; init; }
    public required DateTime OriginDateTime { get; init; }
    public DateTime? DestinationDateTime { get; init; }
    public decimal? MaxPrice { get; init; }
    public DateTime? MinTimeLimit { get; init; }
}