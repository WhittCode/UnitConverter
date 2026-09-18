using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitConverter.Models;

namespace UnitConverter.Pages;

public class ConversionsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string ConversionType { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Input { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Output { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public ConversionModel Conversion { get; set; } = new ConversionModel();

    public string ErrorMessage { get; set; } = string.Empty;


    public void OnGet()
    {
        if (PageContext?.ViewData != null)
        {
            ViewData["Title"] = "Conversions";
        }


        if (RouteData?.Values != null)
        {
            string? routeConversionType = RouteData.Values["conversionType"]?.ToString();
            string? routeInput = RouteData.Values["input"]?.ToString();

            if (!string.IsNullOrEmpty(routeConversionType)) ConversionType = routeConversionType;
            if (!string.IsNullOrEmpty(routeInput)) Input = routeInput;
        }


        if (Conversion != null)
        {
            if (string.IsNullOrEmpty(ConversionType) && !string.IsNullOrEmpty(Conversion.ConversionType))
                ConversionType = Conversion.ConversionType;
            if (string.IsNullOrEmpty(Input) && !string.IsNullOrEmpty(Conversion.Input))
                Input = Conversion.Input;
        }

        if (string.IsNullOrEmpty(ConversionType) && string.IsNullOrEmpty(Input))
        {
            ConversionType = "MilesToKilometers";
            Input = "3.1415";
        }


        if (Conversion == null) Conversion = new ConversionModel();
        Conversion.ConversionType = ConversionType;
        Conversion.Input = Input;


        if (PageContext?.ViewData != null)
        {
            if (ConversionType.Equals("MilesToKilometers", StringComparison.OrdinalIgnoreCase))
            {
                ViewData["ConversionType"] = "Miles to Kilometers";
            }
            else
            {
                ViewData["ConversionType"] = ConversionType;
            }

            ViewData["Title"] = "Conversions";
        }


        if (!double.TryParse(Input, out double numInput))
        {
            ErrorMessage = "Input must be a number.";
            if (PageContext?.ViewData != null) ViewData["ErrorMessage"] = ErrorMessage;
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

            Conversion.Output = Output;
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }
        catch (Exception)
        {
            ErrorMessage = "An error occurred during calculation.";
        }


        if (PageContext?.ViewData != null && !string.IsNullOrEmpty(ErrorMessage))
        {
            ViewData["ErrorMessage"] = ErrorMessage;
        }
    }
}




