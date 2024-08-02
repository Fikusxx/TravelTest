using Domain.Routes;

namespace Application.Routes.Contracts;

public interface IReadRouteRepository
{
    Task<IEnumerable<Route>> GetRoutesByFilter(SearchFilter filter, CancellationToken token = default);
}