using ProviderContracts;

namespace Application.Routes.Queries.GetByFilter;

internal static class GetByFilterQueryMappings
{
    public static ProviderOneSearchRequest ToProviderOneSearchRequest(this GetByFilterQuery query)
    {
        var request = new ProviderOneSearchRequest()
        {
            From = query.Origin,
            To = query.Destination,
            DateFrom = query.OriginDateTime,
            DateTo = query.DestinationDateTime,
            MaxPrice = query.MaxPrice
        };

        return request;
    }
    
    public static ProviderTwoSearchRequest ToProviderTwoSearchRequest(this GetByFilterQuery query)
    {
        var request = new ProviderTwoSearchRequest()
        {
            Departure = query.Origin,
            Arrival = query.Destination,
            DepartureDate = query.OriginDateTime,
            MinTimeLimit = query.MinTimeLimit
        };

        return request;
    }
}