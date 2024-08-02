using Application.Routes.Queries.GetByFilter;
using Application.Routes.Queries.GetCachedByFilterQuery;

namespace Api.Routes.Requests;

public static class SearchRequestMappings
{
    public static GetByFilterQuery MapToGetByFilterQuery(this SearchRequestV1 request)
    {
        var query = new GetByFilterQuery
        {
            Origin = request.Origin,
            Destination = request.Destination,
            OriginDateTime = request.OriginDateTime,
            DestinationDateTime = request.DestinationDateTime,
            MaxPrice = request.MaxPrice,
            MinTimeLimit = request.MinTimeLimit
        };

        return query;
    }
    
    public static GetCachedByFilterQuery MapToGetCachedByFilterQuery(this SearchRequestV1 request)
    {
        var query = new GetCachedByFilterQuery
        {
            Origin = request.Origin,
            Destination = request.Destination,
            OriginDateTime = request.OriginDateTime,
            DestinationDateTime = request.DestinationDateTime,
            MaxPrice = request.MaxPrice,
            MinTimeLimit = request.MinTimeLimit
        };

        return query;
    }
}