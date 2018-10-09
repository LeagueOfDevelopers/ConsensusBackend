using Consensus.Models.SpeakersModels;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        /// <summary>
        ///     Получить топ спикеров
        /// </summary>
        [HttpGet]
        [Route("top")]
        [ProducesResponseType(typeof(GetTopSpeakersResponseModel), 200)]
        public IActionResult GetTopSpeakers()
        {
            return Ok();
        }
    }
}