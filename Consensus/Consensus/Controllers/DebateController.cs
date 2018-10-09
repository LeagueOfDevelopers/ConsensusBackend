using Consensus.Models.DebateModels;
using ConsensusLibrary.DebateContext;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using ConsensusLibrary.Tools;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebateController : ControllerBase
    {
        public DebateController(IDebateFacade debateFacade)
        {
            _debateFacade = Ensure.Any.IsNotNull(debateFacade);
        }

        /// <summary>
        /// Добавляет дебаты
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(AddDebateResponseModel), 200)]
        [ProducesResponseType(401)]
        public IActionResult AddDebate([FromBody] AddDebateRequestModel model)
        {
            var newDebateIdentifier = _debateFacade.CreateDebate(model.StartDateTime, model.EndDateTime, model.Title, new Identifier(model.InviterOpponent),
                new Identifier(model.InvitedOpponent), model.DebateCategory);

            var result = new AddDebateResponseModel(newDebateIdentifier.Id);

            return Ok(result);
        }
        
        /// <summary>
        /// Получить информацию о дебатах по Id
        /// </summary>
        /// <param name="debateId">Id дебатов</param>
        [HttpGet]
        [Route("{debateId}")]
        [ProducesResponseType(typeof(GetDebateResponseModel), 200)]
        public IActionResult GetDebate([FromRoute] Guid debateId)
        {
            var responseView = _debateFacade.GetDebate(new Identifier(debateId));

            var result = new GetDebateResponseModel(responseView.Identifier.Id, responseView.LeftFighterNickName, responseView.LeftFighterId.Id,
                responseView.RightFighterNickName, responseView.RightFighterId.Id, responseView.StartDateTime, responseView.EndDateTime,
                responseView.ViewerCount, responseView.Title, responseView.Category);

            return Ok(result);
        }

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

        private readonly IDebateFacade _debateFacade;
    }
}