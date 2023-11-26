namespace CurrencyConvertor.Conversion;

public class ConvertorException : Exception
{
    public ConvertorException()
    {
    }

    public ConvertorException(string message)
        : base(message)
    {
    }

    public ConvertorException(string message, Exception inner)
        : base(message, inner)
    {
    }
}