namespace CurrencyConvertor.API.Contract;

public class ConvertRequest
{
    public string Value { get; set; }

    //public decimal Value { get; set; } // In JSON only period can be used as separator (e.g. "Value": 999999999.99). 
    //It is also should be possible to use custom ModelBinder to parse values from JSON like "Value": "999999999,99" however I would prefer changing type of property to string.

    public string CurrencyCode { get; set; }

    public string LanguageCode { get; set; }
}