using System.Runtime.CompilerServices;
using System.Text;

namespace CurrencyConvertor.Conversion.Convertors;

public class ConvertorEn : IConvertor
{
    private readonly CurrencyInfoEn _currencyInfo;

    private const int MAX_GROUP_VALUE = 999;

    private readonly string[] _groupNames = { "", " thousand", " million" };
    private readonly string[] _numbers =
    {
        "",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
        "ten",
        "eleven",
        "twelve",
        "thirteen",
        "fourteen",
        "fifteen",
        "sixteen",
        "seventeen",
        "eighteen",
        "nineteen"
    };

    private readonly string[] _tens =
    {
        "",//0
        "teen",//1
        "twenty",
        "thirty",
        "forty",
        "fifty",
        "sixty",
        "seventy",
        "eighty",
        "ninety"
    };

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

        for (int groupIndex = _groupNames.Length - 1; groupIndex >= 0; groupIndex--)
        {
            ConvertGroup(groups[groupIndex], _groupNames[groupIndex], stringBuilder);
            stringBuilder.Append(" ");
        }
        
        return stringBuilder.ToString();
    }

    private void ConvertGroup(int value, string groupName, StringBuilder stringBuilder)
    {
        if (value > MAX_GROUP_VALUE)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Cannon convert group value greater than {MAX_GROUP_VALUE}");
        }

        if (value == 0)
        {
            return;
        }

        var hundreds = value / 100;

        if (hundreds > 0)
        {
            stringBuilder.Append(_numbers[hundreds]);
            stringBuilder.Append(" hundred");
        }

        var rest = value / 10;

        if (hundreds > 0 && rest > 0)
        {
            stringBuilder.Append(" ");
        }

        if (rest > 0 && rest < 20)
        {
            stringBuilder.Append(_numbers[rest]);
        }
        else
        {
            var tens = rest / 10;

            stringBuilder.Append(_tens[tens]);

            rest /= 10;

            if (rest > 0)
            {
                stringBuilder.Append("-");
                stringBuilder.Append(_numbers[rest]);
            }
        }

        stringBuilder.Append(groupName);
    }
}