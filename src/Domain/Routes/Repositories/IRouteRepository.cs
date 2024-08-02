namespace Domain.Routes.Repositories;

/// <summary>
/// No updates defined in domain
/// </summary>
public interface IRouteRepository
{
    Task SaveAsync(IEnumerable<Route> routes, CancellationToken token = default);
}