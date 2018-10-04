using Microsoft.AspNetCore.Mvc;
using Consensus.Models.AccountModels;
using ConsensusLibrary.UserContext;
using EnsureThat;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IRegistrationFacade registrationFacade)
        {
            _registrationFacade = Ensure.Any.IsNotNull(registrationFacade);
        }

        [HttpPost]
        [Route("registration")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(RegistrationResponseModel), 200)]
        public IActionResult Registration([FromBody] RegistrationRequestModel model)
        {
            var newId = _registrationFacade.RegistrateUser(model.Email, model.NickName, model.Password);

            var response = new RegistrationResponseModel(newId.Id);

            return Ok(response);
        }

        private readonly IRegistrationFacade _registrationFacade;
    }
}