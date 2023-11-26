using CurrencyConvertor.API.Contract;
using CurrencyConvertor.Conversion;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertorController : ControllerBase
    {
        private readonly IConvertorFactory _convertorFactory;
        private readonly INumberParser _numberParser;
        private readonly ILogger<ConvertorController> _logger;

        public ConvertorController(IConvertorFactory convertorFactory, INumberParser numberParser, ILogger<ConvertorController> logger)
        {
            _convertorFactory = convertorFactory ?? throw new ArgumentNullException(nameof(convertorFactory));
            _numberParser = numberParser ?? throw new ArgumentNullException(nameof(numberParser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("convert", Name = "ConvertToWords")]
        public async Task<ActionResult<ConvertResponse>> ConvertToWords([FromBody] ConvertRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
             
            //todo: validate request 

            _logger.LogDebug("Converting currency... Value: {value}, CurrencyCode: {currencyCode}, LanguageCode: {languageCode}",
                request.Value,
                request.CurrencyCode,
                request.LanguageCode);

            var valueToConvert = _numberParser.ParseDecimal(request.Value);

            // In a real-world application, there will most likely be a need for the conversion of different currencies and languages.
            // Therefore, I used the IConvertorFactory to create currency- and language-specific converters.
            var convertor = _convertorFactory.GetConvertor(request.CurrencyCode, request.LanguageCode);

            string word;
            try
            {
                word = convertor.ConvertToWord(valueToConvert);
            }
            catch (Exception ex)
            {
                throw new ConvertorException(
                    $"Cannot convert value. Value: {request.Value}, CurrencyCode: {request.CurrencyCode}, LanguageCode: {request.LanguageCode}",
                    ex);
            }

            _logger.LogInformation("Conversion finished. OriginalValue: {value}, Word: {word}, CurrencyCode: {currencyCode}, LanguageCode: {languageCode}",
                request.Value,
                word,
                request.CurrencyCode,
                request.LanguageCode);

            return new ConvertResponse()
            {
                CurrencyCode = request.CurrencyCode,
                OriginalValue = request.Value,
                ConversionResult = word
            };
        }
    }
}
