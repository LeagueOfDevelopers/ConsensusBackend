using Consensus.Models.DebateModels;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebateController : ControllerBase
    {
        /// <summary>
        /// Получить дебаты в эфире
        /// </summary>
        [HttpGet]
        [Route("live")]
        [ProducesResponseType(typeof(GetLiveDebatesResponseModel), 200)]
        public IActionResult GetLiveDebates()
        {
            return Ok();
        }

        /// <summary>
        /// Получить прошедшие дебаты
        /// </summary>
        [HttpGet]
        [Route("past")]
        [ProducesResponseType(typeof(GetPastDebatesResponseModel), 200)]
        public IActionResult GetPastDebates()
        {
            return Ok();
        }
    }
}