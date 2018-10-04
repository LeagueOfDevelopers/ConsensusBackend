using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consensus.Models.SpeakersModels;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        public SpeakersController()
        {
        }

        /// <summary>
        /// Получить топ спикеров
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
