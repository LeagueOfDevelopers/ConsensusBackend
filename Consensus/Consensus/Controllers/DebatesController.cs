using System;
using System.Collections.Generic;
using System.Linq;
using Consensus.Extensions;
using Consensus.Models;
using Consensus.Models.DebateModels;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DebatesController : ControllerBase
    {
        private readonly IDebateFacade _debateFacade;

        public DebatesController(IDebateFacade debateFacade)
        {
            _debateFacade = Ensure.Any.IsNotNull(debateFacade);
        }

        /// <summary>
        ///     Добавляет дебаты
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(AddDebateResponseModel), 200)]
        [ProducesResponseType(401)]
        public IActionResult AddDebate([FromBody] AddDebateRequestModel model)
        {
            var requestedId = Request.GetUserId();

            var newDebateIdentifier = _debateFacade.CreateDebate(model.StartDateTime, model.Title,
                new Identifier(requestedId),
                new Identifier(model.InvitedOpponent), model.DebateCategory);

            var result = new AddDebateResponseModel(newDebateIdentifier.Id);

            return Ok(result);
        }

        /// <summary>
        ///     Получить информацию о дебатах по Id
        /// </summary>
        /// <param name="debateId">Id дебатов</param>
        [HttpGet]
        [Route("{debateId}")]
        [ProducesResponseType(typeof(GetDebateResponseModel), 200)]
        public IActionResult GetDebate([FromRoute] Guid debateId)
        {
            var responseView = _debateFacade.GetDebate(new Identifier(debateId));

            var memberModels = new List<DebateMemberResponseModel>();

            responseView.Members.ToList().ForEach(m =>
            {
                memberModels.Add(new DebateMemberResponseModel(m.NickName, m.Identifier.Id, m.Ready,
                    m.TranslationLink));
            });

            var response = new GetDebateResponseModel(
                responseView.StartDateTime,
                responseView.Title,
                responseView.Category,
                responseView.State, memberModels);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("{debateId}/status")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        [ProducesResponseType(401)]
        public IActionResult SetStatus([FromRoute] Guid debateId)
        {
            var userId = Request.GetUserId();

            _debateFacade.SetReadyStatus(new Identifier(debateId), new Identifier(userId));

            return Ok();
        }

        /// <summary>
        ///     Получить дебаты в эфире
        /// </summary>
        [HttpGet]
        [Route("live")]
        [ProducesResponseType(typeof(GetLiveDebatesResponseModel), 200)]
        public IActionResult GetLiveDebates()
        {
            var liveDebates = _debateFacade.GetLiveDebates();
            var debateItems = new List<GetLiveDebatesResponseItemModel>();
            liveDebates.ToList().ForEach(d => new GetLiveDebatesResponseItemModel(d.Id.Id,
                d.Title, d.FirstDebaterId.Id, d.SecondDebaterId.Id, d.FirstDebaterName, d.SecondDebaterName,
                d.SpectatorsCount, d.Theme, d.Thumbnail));
            var response = new GetLiveDebatesResponseModel(debateItems);

            return Ok(response);
        }

        /// <summary>
        ///     Получить прошедшие дебаты
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