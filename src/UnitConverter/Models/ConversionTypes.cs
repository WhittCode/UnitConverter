using System.Collections.Generic;

namespace UnitConverter.Models;

public class ConversionTypes
{

    public const string MilesToKilometers = "MilesToKilometers";
    public const string KilometersToMiles = "KilometersToMiles";
    public const string FahrenheitToCelsius = "FahrenheitToCelsius";
    public const string CelsiusToFahrenheit = "CelsiusToFahrenheit";
    public const string PoundsToKilograms = "PoundsToKilograms";
    public const string KilogramsToPounds = "KilogramsToPounds";
    public const string TablespoonToTeaspoon = "TablespoonToTeaspoon";
    public const string TeaspoonToTablespoon = "TeaspoonToTablespoon";

    public static readonly IReadOnlyDictionary<string, string> All =
        new Dictionary<string, string>
        {
            [MilesToKilometers] = "Miles to Kilometers",
            [KilometersToMiles] = "Kilometers to Miles",
            [FahrenheitToCelsius] = "Fahrenheit to Celsius",
            [CelsiusToFahrenheit] = "Celsius to Fahrenheit",
            [PoundsToKilograms] = "Pounds to Kilograms",
            [KilogramsToPounds] = "Kilograms to Pounds",
            [TablespoonToTeaspoon] = "Tablespoons to Teaspoons",
            [TeaspoonToTablespoon] = "Teaspoons to Tablespoons"
        };
}
