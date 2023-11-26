using System.Collections;
using CurrencyConvertor.Conversion;
using CurrencyConvertor.Conversion.Convertors;

namespace CurrencyConvertor.Convertor.Tests.Convertors
{
    public class ConvertorEnTests
    {
        [TestCaseSource(nameof(ConvertToWordTestCases))]
        public void ConvertToWord(decimal value, string expectedWord, CurrencyInfoEn currencyInfo)
        {
            var convertor = new ConvertorEn(currencyInfo);

            var actual = convertor.ConvertToWord(value);
            
            Assert.That(actual, Is.EqualTo(expectedWord));
        }

        public static IEnumerable ConvertToWordTestCases
        {
            get
            {
                var currency = "dollar";
                var currencyPlural = "dollars";
                var fractional = "cent";
                var fractionalPlural = "cents";

                var dollarCurrencyInfoEn = new CurrencyInfoEn(currency, currencyPlural, fractional, fractionalPlural);

                yield return new object[] { 0M, $"zero {currencyPlural}", dollarCurrencyInfoEn };
                yield return new object[] { 1M, $"one {currency}", dollarCurrencyInfoEn };
                yield return new object[] { 25.1M, $"twenty-five {currencyPlural} and ten {fractionalPlural}", dollarCurrencyInfoEn };
                yield return new object[] { 0.01M, $"zero {currencyPlural} and one {currency}", dollarCurrencyInfoEn };
                yield return new object[] { 45100M, $"forty-five thousand one hundred {currencyPlural}", dollarCurrencyInfoEn };
                yield return new object[] { 999999999.99M, $"nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine {currencyPlural} and ninety-nine {fractionalPlural}", dollarCurrencyInfoEn };
            }
        }
    }
}