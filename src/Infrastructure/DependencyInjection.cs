using Application.Routes.Contracts;
using Domain.Routes.Repositories;
using Infrastructure.Routes.ProviderSettings;
using Infrastructure.Routes.Repositories;
using Infrastructure.Routes.RouteProviders.ProviderOne;
using Infrastructure.Routes.RouteProviders.ProviderTwo;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddProviderServices();
        services.AddHttpClients();
        services.AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IProviderOneService, ProviderOneService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<ProviderOneSettings>();

            client.BaseAddress = new Uri(settings.Url);
        });
        
        services.AddHttpClient<IProviderTwoService, ProviderTwoService>((sp, client) =>
        {
            var settings = sp.GetRequiredService<ProviderTwoSettings>();

            client.BaseAddress = new Uri(settings.Url);
        });

        return services;
    }

    private static IServiceCollection AddProviderServices(this IServiceCollection services)
    {
        services.AddScoped<IProviderOneService, ProviderOneService>();
        services.AddScoped<IProviderTwoService, ProviderTwoService>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRouteRepository, InMemoryRouteRepository>();
        services.AddSingleton<IReadRouteRepository, InMemoryRouteRepository>();

        return services;
    }
}