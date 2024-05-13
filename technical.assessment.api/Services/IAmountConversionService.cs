namespace technical.assessment.api.Services;

/// <summary>
/// Provides a service for converting an amount.
/// </summary>
public interface IAmountConversionService
{
    /// <summary>
    /// Converts the given amount to words.
    /// </summary>
    /// <param name="amount">The amount to convert.</param>
    /// <returns>The amount in words.</returns>
    string ConvertAmountToWords(string amount);
}