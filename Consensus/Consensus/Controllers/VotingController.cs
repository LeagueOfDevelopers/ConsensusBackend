using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consensus.Extensions;
using Consensus.Models.VotingModels;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        public VotingController(IDebateVotingFacade debateVotingFacade)
        {
            _debateVotingFacade = Ensure.Any.IsNotNull(debateVotingFacade);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(OkObjectResult), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult Vote([FromRoute] VoteRequestModel model)
        {
            var requestedId = Request.GetUserId();
            _debateVotingFacade.Vote(new Identifier(model.DebateId), new Identifier(requestedId), new Identifier(model.ToUser));
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetVotingResultResponseModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult GetVotingResult([FromRoute] GetVotingResultRequestModel model)
        {
            var result = _debateVotingFacade.GetVotingResults(new Identifier(model.DebateId));

            var response = new GetVotingResultResponseModel(result.FirstDedaterVotesTotalCount, result.FirstDedaterNickName, result.FirstDedaterIdentifier,
                result.SecondDedaterVotesTotalCount, result.SecondDedaterNickName, result.SecondDedaterIdentifier);

            return Ok(response);
        }

        private readonly IDebateVotingFacade _debateVotingFacade;
    }
}