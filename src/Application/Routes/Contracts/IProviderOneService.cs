using ProviderContracts;

namespace Application.Routes.Contracts;

public interface IProviderOneService
{
    Task<ProviderOneSearchResponse> GetRouteAsync(ProviderOneSearchRequest request, CancellationToken token = default);
}