using Consensus.Models;
using Consensus.Models.AccountModels;
using Consensus.Security;
using ConsensusLibrary.UserContext;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtIssuer _jwtIssuer;

        private readonly IRegistrationFacade _registrationFacade;
        private readonly IUserProfileFacade _userProfileFacade;

        public AccountController(
            IRegistrationFacade registrationFacade,
            IUserProfileFacade userProfileFacade,
            IJwtIssuer jwtIssuer)
        {
            _registrationFacade = Ensure.Any.IsNotNull(registrationFacade);
            _jwtIssuer = Ensure.Any.IsNotNull(jwtIssuer);
            _userProfileFacade = Ensure.Any.IsNotNull(userProfileFacade);
        }

        [HttpPost]
        [Route("registration")]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 200)]
        public IActionResult Registration([FromBody] RegistrationRequestModel model)
        {
            var newId = _registrationFacade.RegisterUser(model.Email, model.NickName, model.Password);

            var response = new RegistrationResponseModel(newId.Id);

            return Ok(response);
        }

        [HttpPost]
        [Route("auth")]
        [ProducesResponseType(typeof(LoginResponseModel), 200)]
        public IActionResult Login([FromBody] LoginRequestModel model)
        {
            var userIdentifier = _registrationFacade.CheckUserExistence(model.Email, model.Password);

            if (userIdentifier == null) return Unauthorized();

            var currentUserProfile = _userProfileFacade.GetUserProfileForLogin(userIdentifier.Identifier);

            var response = new LoginResponseModel(
                _jwtIssuer.IssueJwt(Claims.Roles.User, userIdentifier.Identifier.Id),
                currentUserProfile.NickName, currentUserProfile.Email,
                currentUserProfile.RegistrationDateTime, currentUserProfile.Avatar);

            return Ok(response);
        }
    }
}