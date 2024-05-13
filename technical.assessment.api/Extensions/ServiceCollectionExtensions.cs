using technical.assessment.api.Services;

namespace technical.assessment.api.Extensions;

/// <summary>
/// Provides extension methods for the IServiceCollection interface.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add all the services to service collection
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Add AmountConversionService to service collection
        services.AddSingleton<IAmountConversionService, AmountConversionService>();
        return services;
    }
}