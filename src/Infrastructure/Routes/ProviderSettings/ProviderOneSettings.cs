using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Routes.ProviderSettings;

public sealed class ProviderOneSettings
{
    [Url]
    public required string Url { get; init; }
}