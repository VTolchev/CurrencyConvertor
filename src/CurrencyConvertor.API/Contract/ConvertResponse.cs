namespace CurrencyConvertor.API.Contract;

public class ConvertResponse
{
    public string OriginalValue { get; set; }

    public string CurrencyCode { get; set; }

    public string ConversionResult { get; set; }

}