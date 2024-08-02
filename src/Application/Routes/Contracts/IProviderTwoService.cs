using ProviderContracts;

namespace Application.Routes.Contracts;

public interface IProviderTwoService
{
    Task<ProviderTwoSearchResponse> GetRouteAsync(ProviderTwoSearchRequest request, CancellationToken token = default);
}