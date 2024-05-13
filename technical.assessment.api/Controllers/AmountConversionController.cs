using Microsoft.AspNetCore.Mvc;
using technical.assessment.api.Models;
using technical.assessment.api.Services;

namespace technical.assessment.api.Controllers;

/// <summary>
/// Controller for converting amount.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AmountConversionController(IAmountConversionService amountConversionService) : ControllerBase
{
    /// <summary>
    /// Convert a dollar amount value to words
    /// e.g, Amount = 123.45, Words = ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>Words</returns>
    [HttpPost]
    public IActionResult ConvertAmountToWords([FromBody]ConvertAmountToWordsRequest request)
    {
        var words = amountConversionService.ConvertAmountToWords(request.Amount);
        return Ok(words);
    }
}