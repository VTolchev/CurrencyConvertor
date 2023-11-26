using CurrencyConvertor.API.Conversion.Convertors;

namespace CurrencyConvertor.API.Conversion;

public interface IConvertorFactory
{
    IConvertor GetConvertor(string currencyCode, string languageCode);
}