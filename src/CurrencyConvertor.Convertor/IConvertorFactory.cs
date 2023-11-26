using CurrencyConvertor.Conversion.Convertors;

namespace CurrencyConvertor.Conversion;

public interface IConvertorFactory
{
    IConvertor GetConvertor(string currencyCode, string languageCode);
}