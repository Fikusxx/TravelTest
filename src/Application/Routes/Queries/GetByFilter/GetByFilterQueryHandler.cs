using System.Collections.Concurrent;
using Application.Routes.Contracts;
using Application.Routes.Dtos;
using Application.Routes.Events;
using Application.Routes.Services;
using Domain.Routes;
using MediatR;
using ProviderContracts;

namespace Application.Routes.Queries.GetByFilter;

internal sealed class GetByFilterQueryHandler : IRequestHandler<GetByFilterQuery, RouteSearchResponseDto>
{
    private readonly IProviderOneService providerOneService;
    private readonly IProviderTwoService providerTwoService;
    private readonly IPublisher publisher;
    private readonly RouteSearchResponseProcessor routeSearchResponseProcessor;

    public GetByFilterQueryHandler(IProviderOneService providerOneService, IProviderTwoService providerTwoService, 
        RouteSearchResponseProcessor routeSearchResponseProcessor, IPublisher publisher)
    {
        this.providerOneService = providerOneService;
        this.providerTwoService = providerTwoService;
        this.routeSearchResponseProcessor = routeSearchResponseProcessor;
        this.publisher = publisher;
    }

    public async Task<RouteSearchResponseDto> Handle(GetByFilterQuery request, CancellationToken cancellationToken)
    {
        var providerOneRequest = request.ToProviderOneSearchRequest();
        var providerOneTask = providerOneService.GetRouteAsync(providerOneRequest, cancellationToken);

        var providerTwoRequest = request.ToProviderTwoSearchRequest();
        var providerTwoTask = providerTwoService.GetRouteAsync(providerTwoRequest, cancellationToken);

        await Task.WhenAll(providerOneTask, providerTwoTask);
        
        var secondProviderResult = providerTwoTask.Result;
        secondProviderResult =
            FilterProviderTwoSearchResponse(secondProviderResult, request.DestinationDateTime, request.MaxPrice);

        var firstProviderResult = providerOneTask.Result;
        firstProviderResult = FilterProviderOneSearchResponse(firstProviderResult, request.MinTimeLimit);

        var routes = ConvertProviderResponsesToRoutes(firstProviderResult, secondProviderResult);

        var response = routeSearchResponseProcessor.CreateRouteSearchResponseDtoFromRoutes(routes);
        
        // save via synchronous event, realistically should be async
        await publisher.Publish(new FilteredRoutesRequestedEvent { Routes = routes }, cancellationToken);

        return response;
    }

    private static ProviderTwoSearchResponse FilterProviderTwoSearchResponse(ProviderTwoSearchResponse response,
        DateTime? destinationDateTime, decimal? maxPrice)
    {
        IEnumerable<ProviderTwoRoute> secondProviderRoutes = response.Routes;

        // в запроса 2го провайдера нет даты конца 
        if (destinationDateTime is not null)
        {
            // значит фильтруем по дате конца ответ от 2го провайдера
            secondProviderRoutes = secondProviderRoutes.Where(x => x.Arrival.Date.Date == destinationDateTime.Value.Date);
        }

        // в запросе 2го провайдера нет максимальной цены
        if (maxPrice is not null)
        {
            // значит фильтруем по максимальной цене ответ от 2го провайдера
            secondProviderRoutes = secondProviderRoutes.Where(x => x.Price <= maxPrice);
        }

        response.Routes = secondProviderRoutes.ToArray();

        return response;
    }

    private static ProviderOneSearchResponse FilterProviderOneSearchResponse(ProviderOneSearchResponse response,
        DateTime? minTimeLimit)
    {
        IEnumerable<ProviderOneRoute> firstProviderRoutes = response.Routes;

        // в запросе 1го провайдере нет time limit
        if (minTimeLimit is not null)
        {
            // значит фильтруем по тайм лимиту ответ от 1го провайдера
            firstProviderRoutes = firstProviderRoutes.Where(x => x.TimeLimit.Date >= minTimeLimit.Value.Date);
        }

        response.Routes = firstProviderRoutes.ToArray();

        return response;
    }

    private ConcurrentBag<Route> ConvertProviderResponsesToRoutes(ProviderOneSearchResponse response1,
        ProviderTwoSearchResponse response2)
    {
        // предполагаю, что они все разные
        // и id в данном случае бессмысленный, тк на стороне сервисов нет уникальности
        var routes = new ConcurrentBag<Route>();

        Parallel.ForEach(response1.Routes, x =>
        {
            var route = x.ToRoute();
            routes.Add(route);
        });

        Parallel.ForEach(response2.Routes, x =>
        {
            var route = x.ToRoute();
            routes.Add(route);
        });

        return routes;
    }
}