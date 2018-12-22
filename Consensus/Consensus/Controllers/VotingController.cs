using System;
using Consensus.Extensions;
using Consensus.Models.VotingModels;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("/debates/{debateId}/[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly IDebateVotingFacade _debateVotingFacade;

        public VotingController(IDebateVotingFacade debateVotingFacade)
        {
            _debateVotingFacade = Ensure.Any.IsNotNull(debateVotingFacade);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult Vote([FromBody] VoteRequestModel model, [FromRoute] Guid debateId)
        {
            var requestedId = Request.GetUserId();
            _debateVotingFacade.Vote(new Identifier(debateId), new Identifier(requestedId),
                new Identifier(model.ToUser));
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetVotingResultResponseModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult GetVotingResult([FromRoute] Guid debateId)
        {
            var result = _debateVotingFacade.GetVotingResults(new Identifier(debateId));

            var response = new GetVotingResultResponseModel(result.FirstDedaterVotesTotalCount,
                result.FirstDedaterNickName, result.FirstDedaterIdentifier,
                result.SecondDedaterVotesTotalCount, result.SecondDedaterNickName, result.SecondDedaterIdentifier);

            return Ok(response);
        }
    }
}