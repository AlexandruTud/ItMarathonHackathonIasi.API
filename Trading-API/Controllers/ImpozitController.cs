using Microsoft.AspNetCore.Mvc;
using Trading_API.Interfaces;

namespace Trading_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ImpozitController : ControllerBase
    {
        private readonly IIpozitRepository _ipozitRepository;
        public ImpozitController(IIpozitRepository ipozitRepository)
        {
            _ipozitRepository = ipozitRepository;
        }
        [HttpGet]
        [Route("totalImpozit")]
        public async Task<IActionResult> GetTotalImpozit()
        {
            var result = await _ipozitRepository.GetTotalImpozit();
            return Ok(result);
        }
    }
}
