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
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("search")]
        public IActionResult GetUserBySectionName([FromQuery] string sectionName)
        {
            var result = _userSearchFacade.SearchUserByName(sectionName);

            var userModels = new List<GetUserBySectionNameResponseItemModel>();

            result.Users.ToList().ForEach(u => userModels.Add(
                new GetUserBySectionNameResponseItemModel(u.UserName, u.UserIdentifier)));

            var response = new GetUserBySectionNameResponseModel(userModels);

            return Ok(response);
        }

        private readonly IUserSearchFacade _userSearchFacade;

        public UsersController(IUserSearchFacade userSearchFacade)
        {
            _userSearchFacade = Ensure.Any.IsNotNull(userSearchFacade);
        }
    }
}