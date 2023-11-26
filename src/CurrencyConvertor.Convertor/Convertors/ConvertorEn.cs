using System.Runtime.CompilerServices;
using System.Text;

namespace CurrencyConvertor.Conversion.Convertors;

public class ConvertorEn : IConvertor
{
    private readonly CurrencyInfoEn _currencyInfo;

    private const int MAX_GROUP_VALUE = 999;


    private const string ZERO = "zero";

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
        "",//0. Not used, but added to fill array item with 0 index.
        "teen",//1. Not used, but added to fill array item with 1 index.
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

        var fractionalPart = (int)((value % 1) * 100);

        ConvertIntegralPart(value, stringBuilder);

        return stringBuilder.ToString();
    }

    private void ConvertIntegralPart(decimal value, StringBuilder stringBuilder)
    {
        var integralPart = (int)Math.Truncate(value);

        if (integralPart == 1)
        {
            stringBuilder.Append(_numbers[integralPart]);
            stringBuilder.Append(' ');
            stringBuilder.Append(_currencyInfo.Name);

            return;
        }

        ConvertIntegralValuePart(integralPart, stringBuilder);

        stringBuilder.Append(_currencyInfo.NamePlural);
    }

    private void ConvertIntegralValuePart(int integralPart, StringBuilder stringBuilder)
    {
        if (integralPart == 0)
        {
            stringBuilder.Append(ZERO);
            stringBuilder.Append(' ');
            return;
        }

        List<int> groups = new();

        var rest = integralPart;
        while (rest > 0)
        {
            groups.Add(rest % 1000);

            rest /= 1000;
        }

        if (groups.Count > _groupNames.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(integralPart), integralPart,
                $"Cannon convert integral value having more than {_groupNames.Length} groups. Actual groups count: {groups.Count}");
        }

        for (int groupIndex = groups.Count - 1; groupIndex >= 0; groupIndex--)
        {
            ConvertGroup(groups[groupIndex], _groupNames[groupIndex], stringBuilder);
        }
    }

    private void ConvertGroup(int value, string groupName, StringBuilder stringBuilder)
    {
        if (value > MAX_GROUP_VALUE)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Cannon convert group value greater than {MAX_GROUP_VALUE}");
        }

        if (value == 0) return;

        var hundreds = value / 100;

        if (hundreds > 0)
        {
            stringBuilder.Append(_numbers[hundreds]);
            stringBuilder.Append(" hundred");
        }

        var rest = value % 100;

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

            rest = rest % 10;

            if (rest > 0)
            {
                stringBuilder.Append("-");
                stringBuilder.Append(_numbers[rest]);
            }
        }

        stringBuilder.Append(groupName); 
        stringBuilder.Append(' ');
    }
}