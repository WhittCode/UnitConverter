using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UnitConverter.Models;
using UnitConverter.Services;

namespace UnitConverter.Pages;

public class QuickConversionsModel : PageModel
{
    private readonly IConversionService _conversionService;

    public QuickConversionsModel(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    public string Output { get; set; } = string.Empty;
    public string ErrorMessage  { get; set; } = string.Empty;

    private IActionResult PerformConversion(string input, string conversionType)
    {
        ErrorMessage = string.Empty;
        Output = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            ErrorMessage = "Input cannot be empty.";
            return Page();
        }

        if (!decimal.TryParse(input, out decimal decimalValue))
        {
            ErrorMessage = "Please enter a valid numeric value.";
            return Page();
        }

        try
        {
            decimal result = _conversionService.Convert(decimalValue, conversionType);
            Output = result.ToString();
        }
        catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
        {
            ErrorMessage = "Conversion failed.";
        }

        return Page();
    }



    public IEnumerable<SelectListItem> PoundOptions =>
    [
        new("1 pound", "1"),
        new("5 pounds", "2"),
        new("10 pounds", "10"),
        new("25 pounds", "25"),
        new("50 pounds", "50")
    ];

    public void OnGet()
    {

    }


    public IActionResult OnGetMilesToKilometers(string input)
    {
        return PerformConversion(input, ConversionTypes.MilesToKilometers);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return PerformConversion(input, ConversionTypes.KilometersToMiles);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return PerformConversion(input, ConversionTypes.FahrenheitToCelsius);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return  PerformConversion(input, ConversionTypes.CelsiusToFahrenheit);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return PerformConversion(input,  ConversionTypes.PoundsToKilograms);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return PerformConversion(input, ConversionTypes.KilogramsToPounds);
    }

    public IActionResult OnGetTablespoonToTeaspoon(string input)
    {
        return PerformConversion(input,  ConversionTypes.TablespoonToTeaspoon);
    }

    public IActionResult OnGetTeaspoonToTablespoon(string input)
    {
        return PerformConversion(input, ConversionTypes.TeaspoonToTablespoon);
    }
}
