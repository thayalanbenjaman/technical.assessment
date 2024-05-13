namespace technical.assessment.api.Services;

/// <summary>
/// Provides a service for converting an amount.
/// </summary>
public class AmountConversionService : IAmountConversionService
{
    private static readonly string[] UnitsWords = ["ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"];
    private static readonly string[] TensWords = ["ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"];

    /// <summary>
    /// Converts the given amount to words.
    /// </summary>
    /// <param name="amount">The amount to convert.</param>
    /// <returns>The amount in words.</returns>
    public string ConvertAmountToWords(string amount)
    {
        var numberParts = amount.Split(".");
        var numberDollars = long.Parse(numberParts[0]);
        var dollarsPrefix = numberParts[0].Length == 1 ? "DOLLAR" : "DOLLARS";
        var wordsDollars = $"{ConvertNumberToWords(numberDollars)} {dollarsPrefix}";
        var wordsParts = new List<string>(3) { wordsDollars };
        if (numberParts.Length > 1)
        {
            var stringCents = numberParts[1].Length == 1 ? $"{numberParts[1]}0" : numberParts[1];
            var numberCents = long.Parse(stringCents);
            if (numberCents != 0)
            {
                var centsPrefix = numberParts[0].Length == 1 ? "CENT" : "CENTS";
                var wordsCents = $"{ConvertNumberToWords(numberCents)} {centsPrefix}";
                wordsParts.AddRange(new []{ "AND", wordsCents });
            }
        }
        return string.Join(" ", wordsParts);
    }

    /// <summary>
    /// Converts the given number to words.
    /// </summary>
    /// <param name="number">The number to convert.</param>
    /// <returns>The number in words.</returns>
    private static string ConvertNumberToWords(long number)
    {
        if (number == 0)
        {
            return GetUnitWord(0);
        }

        var parts = new List<string>(20);
        
        GetParts(parts, ref number, 1_000_000_000_000, "TRILLION");
        GetParts(parts, ref number, 1_000_000_000, "BILLION");
        GetParts(parts, ref number, 1_000_000, "MILLION");
        GetParts(parts, ref number, 1_000, "THOUSAND");
        
        GetPartsInsideThousand(parts, number);
        
        return string.Join(" ", parts);
    }

    /// <summary>
    /// Get the parts of a number to convert it to words.
    /// </summary>
    /// <param name="parts">The list to store the parts of the number in words.</param>
    /// <param name="number">The number to convert.</param>
    /// <param name="divisor">The divisor to divide the number by.</param>
    /// <param name="word">The word to add to the parts list.</param>
    private static void GetParts(List<string> parts, ref long number, long divisor, string word)
    {
        var result = number / divisor;
        if (result == 0)
        {
            return;
        }
        
        GetPartsInsideThousand(parts, result);

        number %= divisor;
        parts.Add(word);
    }

    /// <summary>
    /// Get the parts under a thousand for converting a number to words.
    /// </summary>
    /// <param name="parts">The list of parts to get.</param>
    /// <param name="number">The number to get the parts from.</param>
    private static void GetPartsInsideThousand(List<string> parts, long number)
    {
        if (number >= 100)
        {
            parts.Add(GetUnitWord(number / 100));
            number %= 100;
            parts.Add("HUNDRED");
        }

        if (number == 0)
        {
            return;
        }

        if (parts.Count > 0)
        {
            parts.Add("AND");
        }

        if (number >= 20)
        {
            var tens = TensWords[number / 10];
            var units = number % 10;
            parts.Add(units == 0 ? tens : $"{tens}-{GetUnitWord(units)}");
        }
        else
        {
            parts.Add(GetUnitWord(number));
        }
    }

    /// <summary>
    /// Gets the unit value corresponding to the given number.
    /// </summary>
    /// <param name="number">The number to get the unit value for.</param>
    /// <returns>The unit value in words.</returns>
    private static string GetUnitWord(long number)
    {
        return UnitsWords[number];
    }
}