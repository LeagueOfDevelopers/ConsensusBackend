using Consensus.Models;
using Consensus.Models.ConnectionModels;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Consensus.Controllers
{
    /// <summary>
    /// Всё, связанное с коннектом к openvidu напрямую
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly OptionModel _optionModel;

        public ConnectionController(OptionModel optionModel)
        {
            _optionModel = Ensure.Any.IsNotNull(optionModel);
        }

        /// <summary>
        ///     Создает комнату
        /// </summary>
        [HttpPost]
        [Route("session")]
        [ProducesResponseType(typeof(CreateSessionResponseModel), 200)]
        public IActionResult CreateSession()
        {
            var path = "/api/sessions";
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Basic " + _optionModel.Secret);

            var response = JsonConvert.DeserializeObject<CreateSessionResponseModel>(ExecuteRequest(path, request));

            return Ok(response);
        }

        /// <summary>
        ///     Получить все активные сессии
        /// </summary>
        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(GetAllSessionsResponseModel), 200)]
        public IActionResult GetAllSessions()
        {
            return Ok();
        }

        /// <summary>
        ///     Получить сессию
        /// </summary>
        [HttpGet]
        [Route("session/{sessionId}")]
        public IActionResult GetSession([FromRoute] string sessionId)
        {
            return Ok();
        }

        /// <summary>
        ///     Закрыть сессию
        /// </summary>
        [HttpDelete]
        [Route("session/{sessionId}")]
        public IActionResult CloseSession([FromRoute] string sessionId)
        {
            return Ok();
        }


        private string ExecuteRequest(string path, RestRequest request)
        {
            var _client = new RestClient(_optionModel.OpenviduUrl + path);
            var response = _client.Execute(request);

            return response.Content;
        }
    }
}