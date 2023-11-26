using System.Text;

namespace CurrencyConvertor.Conversion.Convertors;

public class ConvertorEn : IConvertor
{
    private CurrencyInfoEn _currencyInfo;

    public ConvertorEn(CurrencyInfoEn currencyInfo)
    {
        _currencyInfo = currencyInfo ?? throw new ArgumentNullException(nameof(currencyInfo));
    }

    public string ConvertToWord(decimal value)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(
            $"nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine {_currencyInfo.Name}s and ninety-nine {_currencyInfo.FractionalName}s");

        return stringBuilder.ToString();
    }
}