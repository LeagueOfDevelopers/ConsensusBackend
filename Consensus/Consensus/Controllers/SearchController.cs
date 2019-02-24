using System.Collections.Generic;
using System.Linq;
using Consensus.Models.SearchModels;
using ConsensusLibrary.UserContext;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    /// <summary>
    /// Поиск пользователей, дебатов 
    /// </summary>
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        /// <summary>
        /// Поиск пользователей по фрагменту имени
        /// </summary>
        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult GetUserBySectionName([FromQuery] string sectionName)
        {
            var result = _userSearchFacade.SearchUserByName(sectionName);

            var userModels = new List<GetUserBySectionNameResponseItemModel>();

            result.Users.ToList().ForEach(u => userModels.Add(
                new GetUserBySectionNameResponseItemModel(u.UserName, u.UserIdentifier)));

            var response = new GetUserBySectionNameResponseModel(userModels);

            return Ok(response);
        }

        /// <summary>
        /// Поиск пользователей и дебатов
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(GetUserAndDebatesResponseModel), 200)]
        public IActionResult GetUserAndDebates(
            [FromQuery] string sectionName, 
            [FromQuery] string category,
            [FromQuery] bool isLive,
            [FromQuery] int debatePageNumber,
            [FromQuery] int userPageNumber)
        {
            //TODO page size settings

            var result = _userSearchFacade
                .SearchUsersAndDebates(sectionName, category, isLive, 5, debatePageNumber, userPageNumber);

            var userModels = new List<GetUserAndDebatesUserResponseItemModel>();
            var debateModels = new List<GetUserAndDebatesDebateResponseItemModel>();

            result.Users.ToList().ForEach(u => userModels.Add(new GetUserAndDebatesUserResponseItemModel(u.NickName,
                u.UserIdentifier, u.UserAvatarFileName, u.WinCount, u.LossCount)));
            result.Debates.ToList().ForEach(d => debateModels.Add(new GetUserAndDebatesDebateResponseItemModel(
                d.DebateTitle, d.DebateIdentifier, d.IsLive, d.Category, d.InviterNickName, d.InviterIdentifier,
                d.InvitedNickName, d.InvitedIdentifier)));

            var response = new GetUserAndDebatesResponseModel(debateModels, userModels);

            return Ok(response);
        }

        public SearchController(IUserSearchFacade userSearchFacade)
        {
            _userSearchFacade = Ensure.Any.IsNotNull(userSearchFacade);
        }

        private readonly IUserSearchFacade _userSearchFacade;
    }
}
