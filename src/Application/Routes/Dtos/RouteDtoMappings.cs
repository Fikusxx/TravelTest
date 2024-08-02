using Domain.Routes;

namespace Application.Routes.Dtos;

internal static class RouteDtoMappings
{
    public static RouteDto ToRouteDto(this Route route)
    {
        var routeDto = new RouteDto
        {
            Id = route.Id,
            Origin = route.Origin,
            Destination = route.Destination,
            OriginDateTime = route.OriginDateTime,
            DestinationDateTime = route.DestinationDateTime,
            Price = route.Price,
            TimeLimit = route.TimeLimit
        };

        return routeDto;
    }
}