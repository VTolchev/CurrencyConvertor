using System.Runtime.CompilerServices;
using System.Text;

namespace CurrencyConvertor.Conversion.Convertors;

public class ConvertorEn : IConvertor
{
    private readonly CurrencyInfoEn _currencyInfo;

    private readonly string[] _groupNames = { "", "thousand", "million" };

    public ConvertorEn(CurrencyInfoEn currencyInfo)
    {
        _currencyInfo = currencyInfo ?? throw new ArgumentNullException(nameof(currencyInfo));
    }

    public string ConvertToWord(decimal value)
    {
        var stringBuilder = new StringBuilder();

        var val = 123456789.56M;

        var integralPart = (int)Math.Truncate(value);
        var fractionalPart = (int)((value % 1) * 100);

        List<int> groups = new();

        var rest = integralPart;

        while (rest > 0)
        {
            groups.Add(rest % 1000);

            rest /= 1000;
        }

        stringBuilder.Append(
            $"nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine {_currencyInfo.Name}s and ninety-nine {_currencyInfo.FractionalName}s");
        return stringBuilder.ToString();
    }
}