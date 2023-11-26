using CurrencyConvertor.Conversion.Convertors;

namespace CurrencyConvertor.Conversion;

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

        return CreateConvertor(currencyCode, languageCode);
    }

    private static IConvertor CreateConvertor(string currencyCode, string languageCode)
    {
        var currencyInfo = GetCurrencyInfoEn(currencyCode, languageCode);

        return new ConvertorEn(currencyInfo);
    }

    private static CurrencyInfoEn GetCurrencyInfoEn(string currencyCode, string languageCode)
    {
        return new CurrencyInfoEn("dollar", "dollars", "cent", "cents");
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