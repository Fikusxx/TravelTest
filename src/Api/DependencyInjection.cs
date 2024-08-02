using Infrastructure.Routes.ProviderSettings;
using Microsoft.Extensions.Options;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureProviderOptions(this IServiceCollection services)
    {
        services.AddProviderOne()
            .AddProviderTwo();

        return services;
    }

    private static IServiceCollection AddProviderOne(this IServiceCollection services)
    {
        services.AddOptions<ProviderOneSettings>()
            .BindConfiguration(nameof(ProviderOneSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddSingleton(sp =>
        {
            return sp.GetRequiredService<IOptions<ProviderOneSettings>>().Value;
        });

        return services;
    }
    
    private static IServiceCollection AddProviderTwo(this IServiceCollection services)
    {
        services.AddOptions<ProviderTwoSettings>()
            .BindConfiguration(nameof(ProviderTwoSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddSingleton(sp =>
        {
            return sp.GetRequiredService<IOptions<ProviderTwoSettings>>().Value;
        });

        return services;
    }
}