using CurrencyConvertor.API.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Tolchev.CurrencyConvertor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertorController : ControllerBase
    {
        private readonly ILogger<ConvertorController> _logger;

        public ConvertorController(ILogger<ConvertorController> logger)
        {
            _logger = logger;
        }

        [HttpPost("convert", Name = "ConvertToWords")]
        public async Task<ActionResult<ConvertResponse>> ConvertToWords([FromBody] ConvertRequest convertRequest)
        {
            var response =  new ConvertResponse()
            {
                CurrencyCode = convertRequest.CurrencyCode,
                OriginalValue = convertRequest.Value,
                ConversionResult =
                    "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents",
            };

            return response;
        }

 
    }
}
