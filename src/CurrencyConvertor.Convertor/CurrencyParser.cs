using System.Globalization;
using CurrencyConvertor.API;

namespace CurrencyConvertor.Conversion;

public class CurrencyParser : ICurrencyParser
{
    private readonly CultureInfo _cultureInfo;
    private readonly decimal _maximumValue;

    public CurrencyParser(string decimalSeparator, decimal maximumValue)
    {
        if (string.IsNullOrWhiteSpace(decimalSeparator)) throw new ArgumentNullException(nameof(decimalSeparator));
        _maximumValue = maximumValue;

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
    public decimal ParseValue(string input)
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

        if (result > _maximumValue)
        {
            throw new ConvertorException($"Input value greater that maximum supported value. InputValue: {input}, MaximumValue: {_maximumValue}");
        }

        return result;
    }
}