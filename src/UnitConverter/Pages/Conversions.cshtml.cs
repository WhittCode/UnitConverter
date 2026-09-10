using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;

    public void OnGet()
    {

        ViewData["Title"] = "Conversions";

        if (string.IsNullOrEmpty(ConversionType))
        {
            ConversionType = "MilesToKilometers";
        }

        if (string.IsNullOrEmpty(Input))
        {
            Input = "3.1415";
        }

        if (ConversionType.ToLower() == "milestokilometers")
        {
            ViewData["ConversionType"] = "Miles to Kilometers";
        }
        else
        {
            ViewData["ConversionType"] = ConversionType;
        }

        double numInput = 0;

        try
        {
            numInput = Convert.ToDouble(Input);
        }
        catch (FormatException)
        {
            ViewData["ErrorMessage"] = "Input must be a number";
            return;
        }
        catch (OverflowException)
        {
            ViewData["ErrorMessage"] = "Input value is too large or too small.";
            return;
        }

        try
        {
            Output = ConversionType.ToLower() switch
            {
                "milestokilometers" => new UnitOf.Length().FromMiles(numInput).ToKilometers().ToString(),
                "kilometerstomiles" => new UnitOf.Length().FromKilometers(numInput).ToMiles().ToString(),

                "fahrenheittocelsius" => new UnitOf.Temperature().FromFahrenheit(numInput).ToCelsius().ToString(),
                "celsiustofahrenheit" => new UnitOf.Temperature().FromCelsius(numInput).ToFahrenheit().ToString(),

                "poundstokilograms" => new UnitOf.Mass().FromPounds(numInput).ToKilograms().ToString(),
                "kilogramstopounds" => new UnitOf.Mass().FromKilograms(numInput).ToPounds().ToString(),

                "tablespoontoteaspoon" => new UnitOf.Volume().FromTablespoonsUS(numInput).ToTeaspoonsUS().ToString(),
                "teaspoontotablespoon" => new UnitOf.Volume().FromTeaspoonsUS(numInput).ToTablespoonsUS().ToString(),

                _ => throw new InvalidOperationException("Unknown conversion type")
            };
        }
        catch (InvalidOperationException ex)
        {
            ViewData["ErrorMessage"] = ex.Message;
        }
        catch (Exception)
        {
            ViewData["ErrorMessage"] = "An error occurred during calculation";
        }
    }
}
