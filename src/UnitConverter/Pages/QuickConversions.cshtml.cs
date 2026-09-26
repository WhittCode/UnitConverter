using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UnitConverter.Models;

namespace UnitConverter.Pages;

public class QuickConversions : PageModel
{

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

    private IActionResult RedirectToConversion(string conversionType, string input)
    {
        return RedirectToPage("/Conversions", new { conversionType = conversionType, input = input });
    }

    public IActionResult OnGetMilesToKilometers(string input)
    {
        return RedirectToConversion(ConversionTypes.MilesToKilometers, input);
    }

    public IActionResult OnGetKilometersToMiles(string input)
    {
        return RedirectToConversion(ConversionTypes.KilometersToMiles, input);
    }

    public IActionResult OnGetFahrenheitToCelsius(string input)
    {
        return RedirectToConversion(ConversionTypes.FahrenheitToCelsius, input);
    }

    public IActionResult OnGetCelsiusToFahrenheit(string input)
    {
        return  RedirectToConversion(ConversionTypes.CelsiusToFahrenheit, input);
    }

    public IActionResult OnGetPoundsToKilograms(string input)
    {
        return RedirectToConversion(ConversionTypes.PoundsToKilograms, input);
    }

    public IActionResult OnGetKilogramsToPounds(string input)
    {
        return RedirectToConversion(ConversionTypes.KilogramsToPounds, input);
    }

    public IActionResult OnGetTablespoonToTeaspoon(string input)
    {
        return RedirectToConversion(ConversionTypes.TablespoonToTeaspoon, input);
    }

    public IActionResult OnGetTeaspoonToTablespoon(string input)
    {
        return RedirectToConversion(ConversionTypes.TeaspoonToTablespoon, input);
    }
}
