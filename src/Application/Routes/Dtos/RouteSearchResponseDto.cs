namespace Application.Routes.Dtos;

public sealed class RouteSearchResponseDto
{
    public required IEnumerable<RouteDto> Routes { get; init; }
    public required decimal MinPrice { get; init; }
    public required decimal MaxPrice { get; init; }
    public required int MinMinutesRoute { get; init; }
    public required int MaxMinutesRoute { get; init; }
}