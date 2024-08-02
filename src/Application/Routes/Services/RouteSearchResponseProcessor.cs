using Application.Routes.Dtos;
using Domain.Routes;

namespace Application.Routes.Services;

internal sealed class RouteSearchResponseProcessor
{
    public RouteSearchResponseDto CreateRouteSearchResponseDtoFromRoutes(IEnumerable<Route> routes)
    {
        var routesCount = routes.Count();
        
        if (routesCount == 0)
        {
            return new RouteSearchResponseDto
            {
                Routes = Enumerable.Empty<RouteDto>(),
                MinPrice = 0,
                MaxPrice = 0,
                MinMinutesRoute = 0,
                MaxMinutesRoute = 0
            };
        }
        
        var minPrice = decimal.MaxValue;
        var maxPrice = decimal.MinValue;
        var minMinutesRoute = int.MaxValue;
        var maxMinutesRoute = int.MinValue;

        var routeDtoList = new List<RouteDto>(routesCount);

        foreach (var route in routes)
        {
            minPrice = Math.Min(minPrice, route.Price);
            maxPrice = Math.Max(maxPrice, route.Price);
            
            var minutesDelta = (int)(route.DestinationDateTime - route.OriginDateTime).TotalMinutes;
            minMinutesRoute = Math.Min(minMinutesRoute, minutesDelta);
            maxMinutesRoute = Math.Max(maxMinutesRoute, minutesDelta);

            routeDtoList.Add(route.ToRouteDto());
        }

        var routeSearchResponseDto = new RouteSearchResponseDto
        {
            Routes = routeDtoList,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            MinMinutesRoute = minMinutesRoute,
            MaxMinutesRoute = maxMinutesRoute
        };

        return routeSearchResponseDto;
    }
}