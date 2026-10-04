using UnitConverter.Models;

namespace UnitConverter.Services;

public class UnitOfConversionService : IConversionService
{
    public decimal Convert(decimal value, string conversionType)
    {
        double doubleValue = (double)value;

        switch (conversionType)
        {
            case ConversionTypes.MilesToKilometers:
                return (decimal)new UnitOf.Length().FromMiles(doubleValue).ToKilometers();

            case ConversionTypes.KilometersToMiles:
                return (decimal)new UnitOf.Length().FromKilometers(doubleValue).ToMiles();

            case ConversionTypes.FahrenheitToCelsius:
                return (decimal)new UnitOf.Temperature().FromFahrenheit(doubleValue).ToCelsius();

            case ConversionTypes.CelsiusToFahrenheit:
                return (decimal)new UnitOf.Temperature().FromCelsius(doubleValue).ToFahrenheit();

            case ConversionTypes.PoundsToKilograms:
                return (decimal)new UnitOf.Mass().FromPounds(doubleValue).ToKilograms();

            case ConversionTypes.KilogramsToPounds:
                return (decimal)new UnitOf.Mass().FromKilograms(doubleValue).ToPounds();

            case ConversionTypes.TablespoonToTeaspoon:
                return (decimal)new UnitOf.Volume().FromTablespoonsUS(doubleValue).ToTeaspoonsUS();

            case ConversionTypes.TeaspoonToTablespoon:
                return (decimal)new UnitOf.Volume().FromTeaspoonsUS(doubleValue).ToTablespoonsUS();

            default:
                throw new ArgumentException("Unknown conversion type: {conversionType}", nameof(conversionType));
        }
    }
}
