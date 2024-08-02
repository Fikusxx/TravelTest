using Application.Routes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IMarkerAssembly>());

        services.AddSingleton<RouteSearchResponseProcessor>();

        return services;
    }
}