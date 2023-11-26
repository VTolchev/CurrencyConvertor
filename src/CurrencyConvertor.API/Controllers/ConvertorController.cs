using CurrencyConvertor.API.Contract;
using CurrencyConvertor.API.Conversion;
using Microsoft.AspNetCore.Mvc;

namespace Tolchev.CurrencyConvertor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertorController : ControllerBase
    {
        private readonly IConvertorFactory _convertorFactory;
        private readonly ICurrencyParser _currencyParser;
        private readonly ILogger<ConvertorController> _logger;

        public ConvertorController(IConvertorFactory convertorFactory, ICurrencyParser currencyParser, ILogger<ConvertorController> logger)
        {
            _convertorFactory = convertorFactory ?? throw new ArgumentNullException(nameof(convertorFactory));
            _currencyParser = currencyParser ?? throw new ArgumentNullException(nameof(currencyParser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("convert", Name = "ConvertToWords")]
        public async Task<ActionResult<ConvertResponse>> ConvertToWords([FromBody] ConvertRequest convertRequest)
        {
            if (convertRequest == null) throw new ArgumentNullException(nameof(convertRequest));
            //todo: validate convertRequest 

            _logger.LogDebug("Converting currency... Value: {value}, CurrencyCode: {currencyCode}, LanguageCode: {languageCode}",
                convertRequest.Value,
                convertRequest.CurrencyCode,
                convertRequest.LanguageCode);

            var valueToConvert = _currencyParser.ParseValue(convertRequest.Value);

            // In a real-world application, there will most likely be a need for the conversion of different currencies and languages.
            // Therefore, I used the IConvertorFactory to create currency- and language-specific converters.
            var convertor = _convertorFactory.GetConvertor(convertRequest.CurrencyCode, convertRequest.LanguageCode);

            var word = convertor.ConvertToWord(valueToConvert);

            _logger.LogInformation("Conversion finished. OriginalValue: {value}, Word: {word}, CurrencyCode: {currencyCode}, LanguageCode: {languageCode}",
                convertRequest.Value,
                word,
                convertRequest.CurrencyCode,
                convertRequest.LanguageCode);

            return new ConvertResponse()
            {
                CurrencyCode = convertRequest.CurrencyCode,
                OriginalValue = convertRequest.Value,
                ConversionResult = word
            };
        }
    }
}
