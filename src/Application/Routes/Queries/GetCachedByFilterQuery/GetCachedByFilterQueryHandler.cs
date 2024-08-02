using Application.Routes.Contracts;
using Application.Routes.Dtos;
using Application.Routes.Services;
using MediatR;

namespace Application.Routes.Queries.GetCachedByFilterQuery;

internal sealed class GetCachedByFilterQueryHandler : IRequestHandler<GetCachedByFilterQuery, RouteSearchResponseDto>
{
    private readonly IReadRouteRepository readRouteRepository;
    private readonly RouteSearchResponseProcessor routeSearchResponseProcessor;

    public GetCachedByFilterQueryHandler(IReadRouteRepository readRouteRepository, RouteSearchResponseProcessor routeSearchResponseProcessor)
    {
        this.readRouteRepository = readRouteRepository;
        this.routeSearchResponseProcessor = routeSearchResponseProcessor;
    }

    public async Task<RouteSearchResponseDto> Handle(GetCachedByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = new SearchFilter
        {
            Origin = request.Origin,
            Destination = request.Destination,
            OriginDateTime = request.OriginDateTime,
            DestinationDateTime = request.DestinationDateTime,
            MaxPrice = request.MaxPrice,
            MinTimeLimit = request.MinTimeLimit
        };

        var routes = await readRouteRepository.GetRoutesByFilter(filter, cancellationToken);

        var response = routeSearchResponseProcessor.CreateRouteSearchResponseDtoFromRoutes(routes);

        return response;
    }
}