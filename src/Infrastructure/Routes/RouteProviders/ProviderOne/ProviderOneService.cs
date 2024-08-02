using System.Net.Http.Json;
using System.Text.Json;
using Application.Routes.Contracts;
using Domain.Routes;
using ProviderContracts;

namespace Infrastructure.Routes.RouteProviders.ProviderOne;

internal sealed class ProviderOneService : IProviderOneService
{
    private readonly HttpClient httpClient;

    public ProviderOneService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ProviderOneSearchResponse> GetRouteAsync(ProviderOneSearchRequest request, CancellationToken token = default)
    {
        // return Task.FromResult(new ProviderOneSearchResponse()
        // {
        //     Routes = new ProviderOneRoute[1]{ new ProviderOneRoute()
        //     {
        //         From = "Moscow",
        //         To = "Sochi",
        //         DateFrom = DateTime.Now.AddDays(5),
        //         DateTo = DateTime.Now.AddDays(9),
        //         Price = 100,
        //         TimeLimit = DateTime.Now.AddDays(4)
        //     }}
        // });
        var response = await httpClient.PostAsJsonAsync(string.Empty, request, cancellationToken: token);
        var content = await response.Content.ReadAsStringAsync(token);
        var result = JsonSerializer.Deserialize<ProviderOneSearchResponse>(content);
        
        return result!;
    }
}