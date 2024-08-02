using Domain.Routes;
using ProviderContracts;

namespace Application.Routes.Queries.GetByFilter;

internal static class RouteMappings
{
    public static Route ToRoute(this ProviderOneRoute providerOneRoute)
    {
        var route = new Route
        {
            Id = Guid.NewGuid(),
            Origin = providerOneRoute.From,
            Destination = providerOneRoute.To,
            OriginDateTime = providerOneRoute.DateFrom,
            DestinationDateTime = providerOneRoute.DateTo,
            Price = providerOneRoute.Price,
            TimeLimit = providerOneRoute.TimeLimit
        };

        return route;
    }
    
    public static Route ToRoute(this ProviderTwoRoute providerTwoRoute)
    {
        var route = new Route
        {
            Id = Guid.NewGuid(),
            Origin = providerTwoRoute.Departure.Point,
            Destination = providerTwoRoute.Arrival.Point,
            OriginDateTime = providerTwoRoute.Departure.Date,
            DestinationDateTime = providerTwoRoute.Arrival.Date,
            Price = providerTwoRoute.Price,
            TimeLimit = providerTwoRoute.TimeLimit
        };

        return route;
    }
}