using System.Globalization;
using CurrencyConvertor.API.Conversion.Convertors;

namespace CurrencyConvertor.API.Conversion;

public class ConvertorFactory : IConvertorFactory
{
    public IConvertor GetConvertor(string currencyCode, string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode)) throw new ArgumentNullException(nameof(languageCode));
        if (string.IsNullOrWhiteSpace(currencyCode)) throw new ArgumentNullException(nameof(currencyCode));

        // Instead of creating on each request, dictionary can be used.
        // In this case, IsLanguageSupported and IsCurrencySupported are not needed.

        if (!IsLanguageSupported(languageCode))
        {
            throw new InvalidOperationException($"Language is not supported. LanguageCode: {languageCode}");
        }

        if (!IsCurrencySupported(currencyCode))
        {
            throw new InvalidOperationException($"Currency is not supported. CurrencyCode: {currencyCode}");
        }

        var currencyInfo = GetCurrencyInfo(currencyCode, languageCode);

        return CreateConvertor(languageCode, currencyInfo);
    }

    private static IConvertor CreateConvertor(string languageCode, CurrencyInfo currencyInfo)
    {
        return new ConvertorEn(currencyInfo);
    }

    private static CurrencyInfo GetCurrencyInfo(string currencyCode, string languageCode)
    {
        return new CurrencyInfo("dollar", "cent");
    }

    private bool IsLanguageSupported(string languageCode)
    {
        return "en".Equals(languageCode, StringComparison.OrdinalIgnoreCase);
    }

    private bool IsCurrencySupported(string currencyCode)
    {
        return "USD".Equals(currencyCode, StringComparison.OrdinalIgnoreCase);
    }
}