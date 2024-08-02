using Domain.Routes;
using MediatR;

namespace Application.Routes.Events;

internal sealed class FilteredRoutesRequestedEvent : INotification
{
    public required IEnumerable<Route> Routes { get; init; }
}