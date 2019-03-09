using System;
using System.Collections.Generic;
using System.Linq;
using Consensus.Models.SearchModels;
using Consensus.Security;
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
        /// <param name="sectionName">Фрагмент имени</param>
        /// <param name="pageNumber">Номер страницы</param>
        [Authorize]
        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(GetUserBySectionNameResponseModel), 200)]
        public IActionResult GetUserBySectionName(
            [FromQuery] string sectionName,
            [FromQuery] int pageNumber = 1)
        {
            Ensure.Bool.IsTrue(pageNumber > 0, nameof(pageNumber),
                opt => opt.WithException(new ArgumentException()));

            var result = _userSearchFacade.SearchUserByName(
                sectionName,
                _paginationSettings.PageSize,
                pageNumber);

            var userModels = new List<GetUserBySectionNameResponseItemModel>();

            result.Users.ToList().ForEach(u => userModels.Add(
                new GetUserBySectionNameResponseItemModel(u.UserName, u.UserIdentifier)));

            var response = new GetUserBySectionNameResponseModel(userModels);

            return Ok(response);
        }

        /// <summary>
        /// Поиск пользователей и дебатов
        /// </summary>
        /// <param name="sectionName">Фрагмент имени</param>
        /// <param name="category">Категория дебатов</param>
        /// <param name="isLive">Флаг в эфире</param>
        /// <param name="userPageNumber">Номер страницы пользователей</param>
        /// <param name="debatePageNumber">Номер страницы дебатов</param>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(GetUserAndDebatesResponseModel), 200)]
        public IActionResult GetUserAndDebates(
            [FromQuery] string sectionName, 
            [FromQuery] string category,
            [FromQuery] bool isLive,
            [FromQuery] int userPageNumber = 1,
            [FromQuery] int debatePageNumber = 1)
        {

            Ensure.Bool.IsTrue(userPageNumber > 0, nameof(userPageNumber),
                opt => opt.WithException(new ArgumentException()));

            Ensure.Bool.IsTrue(debatePageNumber > 0, nameof(debatePageNumber),
                opt => opt.WithException(new ArgumentException()));

            var result = _userSearchFacade
                .SearchUsersAndDebates(sectionName, category, isLive, _paginationSettings.PageSize, debatePageNumber, userPageNumber);

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

        public SearchController(IUserSearchFacade userSearchFacade, PaginationSettings paginationSettings)
        {
            _userSearchFacade = Ensure.Any.IsNotNull(userSearchFacade);
            _paginationSettings = Ensure.Any.IsNotNull(paginationSettings);
        }

        private readonly IUserSearchFacade _userSearchFacade;
        private readonly PaginationSettings _paginationSettings;
    }
}
