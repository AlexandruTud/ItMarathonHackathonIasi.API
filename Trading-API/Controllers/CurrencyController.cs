using Microsoft.AspNetCore.Mvc;
using Trading_API.Interfaces;
using Trading_API.Requests;
using Trading_API.Resposes;

namespace Trading_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController  : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        [HttpGet]
        [Route("Get_currency")]
        public async Task<IEnumerable<Resposes.CurrencyResponse>> GetCurrencies()
        {
            return await _currencyRepository.GetCurrencies();
        }

        [HttpPost]
        [Route("Make_transaction")]
        public async Task<IActionResult> MakeTransaction(TransactionRequest request)
        {
            var result = await _currencyRepository.MakeTransaction(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("Add_Currency")]
        public async Task<IActionResult> AddCurrency(CurrencyRequest request)
        {
            var result = await _currencyRepository.AddUserCurrency(request);
            if(!result)
                return BadRequest();

            return Ok(result);
        }
        [HttpPatch]
        [Route("Update_ratio")]
        public IActionResult UpdateRatio()
        {
            return Ok();
        }      
    }
}
