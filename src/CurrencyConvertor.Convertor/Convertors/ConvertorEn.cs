using System.Text;

namespace CurrencyConvertor.Conversion.Convertors;

public class ConvertorEn : IConvertor
{
    private readonly CurrencyInfoEn _currencyInfo;

    private const int MAX_GROUP_VALUE = 999;


    private const string ZERO_WORD = "zero ";
    private const string AND_WORD = " and ";

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

        ConvertIntegralPart(value, stringBuilder);
        ConvertFractionalPart(value, stringBuilder);

        return stringBuilder.ToString();
    }

    private void ConvertIntegralPart(decimal value, StringBuilder stringBuilder)
    {
        var integralPart = (int)Math.Truncate(value);

        ConvertIntegralValue(integralPart, _currencyInfo.Name, _currencyInfo.NamePlural, stringBuilder);
    }

    private void ConvertFractionalPart(decimal value, StringBuilder stringBuilder)
    {
        var fractionalPart = (int)((value % 1) * 100);

        if (fractionalPart == 0) return;

        stringBuilder.Append(AND_WORD);

        ConvertIntegralValue(fractionalPart, _currencyInfo.FractionalName, _currencyInfo.FractionalNamePlural, stringBuilder);
    }

    private void ConvertIntegralValue(int integralPart, string nameSingular, string namePlural, StringBuilder stringBuilder)
    {
        if (integralPart == 1)
        {
            stringBuilder.Append(_numbers[integralPart]);
            stringBuilder.Append(' ');
            stringBuilder.Append(nameSingular);

            return;
        }

        ConvertIntegralNumberValue(integralPart, stringBuilder);

        stringBuilder.Append(namePlural);
    }

    private void ConvertIntegralNumberValue(int integralPart, StringBuilder stringBuilder)
    {
        if (integralPart == 0)
        {
            stringBuilder.Append(ZERO_WORD);
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

        ConvertTwoDigitNumber(stringBuilder, hundreds, rest);

        stringBuilder.Append(groupName); 
        stringBuilder.Append(' ');
    }

    private void ConvertTwoDigitNumber(StringBuilder stringBuilder, int hundreds, int value)
    {
        if (hundreds > 0 && value > 0)
        {
            stringBuilder.Append(' ');
        }

        if (value > 0 && value < 20)
        {
            stringBuilder.Append(_numbers[value]);
        }
        else
        {
            var tens = value / 10;

            stringBuilder.Append(_tens[tens]);

            value = value % 10;

            if (value > 0)
            {
                stringBuilder.Append("-");
                stringBuilder.Append(_numbers[value]);
            }
        }
    }
}