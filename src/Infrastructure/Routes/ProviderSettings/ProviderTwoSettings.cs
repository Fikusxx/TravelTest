using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Routes.ProviderSettings;

public sealed class ProviderTwoSettings
{
    [Url]
    public required string Url { get; init; }
}