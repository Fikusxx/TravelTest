using System.Net.Http.Json;
using System.Text.Json;
using Application.Routes.Contracts;
using ProviderContracts;

namespace Infrastructure.Routes.RouteProviders.ProviderTwo;

internal sealed class ProviderTwoService : IProviderTwoService
{
    private readonly HttpClient httpClient;

    public ProviderTwoService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ProviderTwoSearchResponse> GetRouteAsync(ProviderTwoSearchRequest request,
        CancellationToken token = default)
    {
        // return Task.FromResult( new ProviderTwoSearchResponse()
        // {
        //     Routes = new ProviderTwoRoute[1]
        //     {
        //         new ProviderTwoRoute()
        //         {
        //             Departure = new ProviderTwoPoint() { Point = "Moscow", Date = DateTime.Now.AddDays(5) },
        //             Arrival = new ProviderTwoPoint() { Point = "Sochi", Date = DateTime.Now.AddDays(10) },
        //             Price = 99,
        //             TimeLimit = DateTime.Now.AddDays(4)
        //         }
        //     }
        // });
        var response = await httpClient.PostAsJsonAsync(string.Empty, request, cancellationToken: token);
        var content = await response.Content.ReadAsStringAsync(token);
        var result = JsonSerializer.Deserialize<ProviderTwoSearchResponse>(content);
        
        return result!;
    }
}