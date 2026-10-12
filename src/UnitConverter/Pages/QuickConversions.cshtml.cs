using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UnitConverter.Models;
using UnitConverter.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace UnitConverter.Pages;

public class QuickConversionsModel : PageModel
{
    private readonly IConversionService _conversionService;
    private readonly ILogger<QuickConversionsModel> _logger;

    public QuickConversionsModel(IConversionService conversionService, ILogger<QuickConversionsModel> logger)
    {
        _conversionService = conversionService;
        _logger = logger;
    }

    // Properties used by the razor page template
    public string Output { get; set; } = string.Empty;
    public string ErrorMessage  { get; set; } = string.Empty;

    // FIX: Modified matching select list options to use valid numeric decimal values
    public IEnumerable<SelectListItem> PoundOptions => [
        new SelectListItem("1 pound", "1.0"),
        new SelectListItem("5 pounds", "5.0"),
        new SelectListItem("10 pounds", "10.0"),
        new SelectListItem("25 pounds", "25.0"),
        new SelectListItem("50 pounds", "50.0")
    ];

    public void OnGet()
    {
        // Keeps page initialization clear
    }

    private IActionResult PerformConversion(string input, string conversionType)
    {
        ErrorMessage = string.Empty;
        Output = string.Empty;

        _logger.LogInformation("Conversion request received for {ConversionType}", conversionType);

        if (string.IsNullOrWhiteSpace(input))
        {
            ErrorMessage = "Input cannot be empty.";
            _logger.LogWarning("Input is missing or invalid: {Input} for {ConversionType}", input, conversionType);
            return Page();
        }

        // Parse input safely into decimal layout for the service engine
        if (!decimal.TryParse(input, out decimal decimalValue))
        {
            ErrorMessage = "Please enter a valid numeric value.";
            _logger.LogWarning("Input is missing or invalid: {Input} for {ConversionType}", input, conversionType);
            return Page();
        }

        try
        {
            // Execute calculation via the injected business service layer
            decimal result = _conversionService.Convert(decimalValue, conversionType);
            Output = result.ToString();

            _logger.LogInformation(
                "Converted {Input} using {ConversionType} with result {Result}",
                input,
                conversionType,
                result
            );
        }
        catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
        {
            ErrorMessage = "Conversion failed.";
            _logger.LogWarning("Invalid conversion operation attempted: {ConversionType} with input {Input}", conversionType, input);
        }
        catch (Exception ex)
        {
            ErrorMessage = "An unexpected error occurred during calculation.";
            _logger.LogError(ex, "An unexpected conversion failure occurs while processing {Input} for {ConversionType}", input, conversionType);
        }

        return Page();
    }

    // Handlers called directly by forms using asp-page-handler
    public IActionResult OnGetMilesToKilometers(string input) => PerformConversion(input, "MilesToKilometers");
    public IActionResult OnGetKilometersToMiles(string input) => PerformConversion(input, "KilometersToMiles");
    public IActionResult OnGetFahrenheitToCelsius(string input) => PerformConversion(input, "FahrenheitToCelsius");
    public IActionResult OnGetCelsiusToFahrenheit(string input) => PerformConversion(input, "CelsiusToFahrenheit");
    public IActionResult OnGetPoundsToKilograms(string input) => PerformConversion(input, "PoundsToKilograms");
    public IActionResult OnGetKilogramsToPounds(string input) => PerformConversion(input, "KilogramsToPounds");
    public IActionResult OnGetTablespoonToTeaspoon(string input) => PerformConversion(input, "TablespoonToTeaspoon");
    public IActionResult OnGetTeaspoonToTablespoon(string input) => PerformConversion(input, "TeaspoonToTablespoon");
}

