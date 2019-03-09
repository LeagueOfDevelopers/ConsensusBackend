using Consensus.Extensions;
using Consensus.Models;
using Consensus.Models.ProfileModels;
using ConsensusLibrary.Tools;
using ConsensusLibrary.UserContext;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consensus.Controllers
{
    [Route("users/[controller]")]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Возвращает профиль текущего пользователя
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(GetUserProfileResponseModel), 200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult GetUserProfile()
        {
            var currentUserId = Request.GetUserId();

            var result = _userProfileFacade.GetUserProfile(new Identifier(currentUserId));

            var response = new GetUserProfileResponseModel(
                result.Name, result.Avatar, result.About, result.Reputation, result.Email);

            return Ok(response);
        }

        /// <summary>
        /// Изменяет имя (nickname)
        /// </summary>
        [Route("name")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult EditName([FromBody] EditNameRequestModel model)
        {
            var currentUserId = Request.GetUserId();

            _userProfileFacade.ChangeUserName(new Identifier(currentUserId), model.NewName);

            return Ok();
        }

        /// <summary>
        /// Изменяет email
        /// </summary>
        [Route("email")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult EditEmail([FromBody] EditEmailRequestModel model)
        {
            var currentUserId = Request.GetUserId();

            _userProfileFacade.ChangeUserEmail(new Identifier(currentUserId), model.NewEmail);

            return Ok();
        }

        /// <summary>
        /// Изменяет пароль
        /// </summary>
        [Route("password")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult EditPassword([FromBody] EditPasswordRequestModel model)
        {
            var currentUserId = Request.GetUserId();

            _userProfileFacade.ChangeUserPassword(new Identifier(currentUserId), model.NewPassword);

            return Ok();
        }

        /// <summary>
        /// Изменяет поле "О себе"
        /// </summary>
        [Route("about")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult EditAbout([FromBody] EditAboutRequestModel model)
        {
            var currentUserId = Request.GetUserId();

            _userProfileFacade.ChangeUserAbout(new Identifier(currentUserId), model.NewAbout);

            return Ok();
        }

        /// <summary>
        /// Изменяет аватар
        /// </summary>
        [Route("avatar")]
        [HttpPut]
        [Authorize]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public IActionResult EditAvatar([FromBody] EditAvatarRequestModel model)
        {
            var currentUserId = Request.GetUserId();

            _userProfileFacade.ChangeUserAvatar(new Identifier(currentUserId), model.NewAvatar);

            return Ok();
        }

        private readonly IUserProfileFacade _userProfileFacade;

        public ProfileController(IUserProfileFacade userProfileFacade)
        {
            _userProfileFacade = Ensure.Any.IsNotNull(userProfileFacade);
        }
    }
}
