using Domain.Routes.Repositories;
using MediatR;

namespace Application.Routes.Events;

internal sealed class FilteredRoutesRequestedEventHandler : INotificationHandler<FilteredRoutesRequestedEvent>
{
    private readonly IRouteRepository routeRepository;

    public FilteredRoutesRequestedEventHandler(IRouteRepository routeRepository)
    {
        this.routeRepository = routeRepository;
    }

    public Task Handle(FilteredRoutesRequestedEvent notification, CancellationToken cancellationToken)
    {
        return routeRepository.SaveAsync(notification.Routes, cancellationToken);
    }
}