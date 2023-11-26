using System.Globalization;

namespace CurrencyConvertor.Conversion;

public class NumberParser : INumberParser
{
    private readonly CultureInfo _cultureInfo;

    public NumberParser(string decimalSeparator)
    {
        if (string.IsNullOrWhiteSpace(decimalSeparator)) throw new ArgumentNullException(nameof(decimalSeparator));

        CultureInfo culture = GetCulture(decimalSeparator);

        _cultureInfo = culture;
    }

    private static CultureInfo GetCulture(string numberDecimalSeparator)
    {
        var culture = CultureInfo.CreateSpecificCulture(CultureInfo.InvariantCulture.Name);
        culture.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;
        return culture;
    }

    /// <summary>
    /// Converts the string representation of a <paramref name="input"/> to its Decimal equivalent.
    /// </summary>
    /// <param name="input">The string representation of the number to convert.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="input"/> is null.</exception>
    /// <exception cref="ConvertorException"><paramref name="input"/> is not in the correct format. or greater than maximum supported value</exception>
    public decimal ParseDecimal(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));

        decimal result;

        try
        {
            result = decimal.Parse(input, _cultureInfo);
        }
        catch (Exception e)
        {
            throw new ConvertorException($"Cannot parse value. Value: {input}", e);
        }

        return result;
    }
}