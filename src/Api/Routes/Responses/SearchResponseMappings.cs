using Application.Routes.Dtos;

namespace Api.Routes.Responses;

public static class SearchResponseMappings
{
    public static SearchResponseV1 MapToSearchResponseV1(this RouteSearchResponseDto request)
    {
        var result = new SearchResponseV1
        {
            Routes = request.Routes,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            MinMinutesRoute = request.MinMinutesRoute,
            MaxMinutesRoute = request.MaxMinutesRoute
        };

        return result;
    }
}