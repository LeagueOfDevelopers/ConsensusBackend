﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consensus.Extensions;
using Consensus.Models.ChatModels;
using ConsensusLibrary.DebateContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public ChatController(IChatFacade chatFacade)
        {
            _chatFacade = Ensure.Any.IsNotNull(chatFacade);
        }

        [Authorize]
        [HttpPost]
        [Route("message")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(SendMessageResponseModel), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult SendMessage([FromBody] SendMessageRequestModel model)
        {
            var requestedId = Request.GetUserId();

            var messageId = _chatFacade.SendMessage(new Identifier(requestedId), new Identifier(model.debateId), model.Text);

            var response = new SendMessageResponseModel(messageId.Id);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("message")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(GetMessagesResponseModel), 200)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        public IActionResult GetMessages([FromBody] GetMessagesRequestModel model)
        {
            var views = _chatFacade.GetMessages(new Identifier(model.DebateId));

            var resultItems = new List<GetMessagesResponseItemModel>();

            views.ToList().ForEach(v =>
            {
                resultItems.Add(new GetMessagesResponseItemModel(v.MessageId.Id, v.UserId.Id, v.SentOn, v.Text, v.UserName));
            });

            var result = new GetMessagesResponseModel(resultItems);

            return Ok(result);
        }

        private readonly IChatFacade _chatFacade;
    }
}