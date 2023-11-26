namespace CurrencyConvertor.API.Conversion;

public interface ICurrencyParser
{
    decimal ParseValue(string input);
}