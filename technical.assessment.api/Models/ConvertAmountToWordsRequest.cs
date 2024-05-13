namespace technical.assessment.api.Models;

/// <summary>
/// Represents a request to convert an amount to words.
/// </summary>
public class ConvertAmountToWordsRequest
{
    /// <summary>
    /// Represents an amount value that can be converted to words.
    /// </summary>
    public required string Amount { get; set; }
}