namespace CurrencyConvertor.Conversion;

public interface ICurrencyParser
{
    decimal ParseValue(string input);
}