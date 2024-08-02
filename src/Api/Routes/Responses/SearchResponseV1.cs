using Application.Routes.Dtos;

namespace Api.Routes.Responses;

public class SearchResponseV1
{
    public required IEnumerable<RouteDto> Routes { get; init; }
    public required decimal MinPrice { get; init; }
    public required decimal MaxPrice { get; init; }
    public required int MinMinutesRoute { get; init; }
    public required int MaxMinutesRoute { get; init; }
}