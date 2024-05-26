using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Trading_API.Interfaces;
using Trading_API.Requests;

namespace Trading_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoldController : ControllerBase
    {
        private readonly ISoldRepository _soldRepository;
        public SoldController(ISoldRepository soldRepository)
        {
            _soldRepository = soldRepository;
        }
        [HttpPatch]
        [Route("UpdateSold")]
        public async Task<IActionResult> UpdateSold(UpdateSoldRequest updateSoldRequest)
        {
            var result = await _soldRepository.UpdateSold(updateSoldRequest);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetSold")]
        public async Task<IActionResult> GetSold(int IdUser)
        {
            var result = await _soldRepository.GetSold(IdUser);

            return Ok(result);
        }
    }
}
