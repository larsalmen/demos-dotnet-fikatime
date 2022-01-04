using fikatime_api.Models;
using fikatime_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace fikatime_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FikatimeController : ControllerBase
    {
        private readonly FikatimeService _fikatimeService;
        public FikatimeController(FikatimeService fikatimeService)
        {
            _fikatimeService = fikatimeService;
        }


        [HttpGet("{month}")]
        public async Task<IActionResult> GetMonth(int month)
        {
            var fikatimes = await _fikatimeService.GetAllFikatimesForSpecificMonth(month);

            return fikatimes.Any() == true ? Ok(fikatimes) : NoContent();
        }

    }
}
