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
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Возвращает список пользователей по фрагменту имени
        /// </summary>
        /// <param name="sectionName">фрагмент имени</param>
        /// <param name="pageNumber">страница</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("search")]
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

        private readonly IUserSearchFacade _userSearchFacade;
        private readonly PaginationSettings _paginationSettings;

        public UsersController(
            IUserSearchFacade userSearchFacade,
            PaginationSettings paginationSettings)
        {
            _userSearchFacade = Ensure.Any.IsNotNull(userSearchFacade);
            _paginationSettings = Ensure.Any.IsNotNull(paginationSettings);
        }
    }
}