using System.Collections.Concurrent;
using Application.Routes.Contracts;
using Domain.Routes;
using Domain.Routes.Repositories;

namespace Infrastructure.Routes.Repositories;

/// <summary>
/// Imagine this is real db partitioned by DateTo
/// </summary>
internal sealed class InMemoryRouteRepository : IRouteRepository, IReadRouteRepository
{
    private static ConcurrentBag<Route> routesCollection = new();

    public Task SaveAsync(IEnumerable<Route> routes, CancellationToken token = default)
    {
        foreach (var route in routes)
        {
            routesCollection.Add(route);
        }
        
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Route>> GetRoutesByFilter(SearchFilter filter, CancellationToken token = default)
    {
        // would have been IQueryable<T> query
        var result = routesCollection
            .Where(x => x.Origin == filter.Origin)
            .Where(x => x.Destination == filter.Destination)
            .Where(x => x.OriginDateTime.Date == filter.OriginDateTime.Date);

        if (filter.DestinationDateTime is not null)
        {
            result = result.Where(x => x.DestinationDateTime.Date == filter.DestinationDateTime.Value.Date);
        }
        
        if (filter.MaxPrice is not null)
        {
            result = result.Where(x => x.Price <= filter.MaxPrice);
        }
        
        if (filter.MinTimeLimit is not null)
        {
            result = result.Where(x => x.TimeLimit.Date >= filter.MinTimeLimit.Value.Date);
        }

        return Task.FromResult(result);
    }
}