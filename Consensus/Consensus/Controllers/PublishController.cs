using Consensus.Models;
using Consensus.Models.PublishModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Consensus.Controllers
{
    /// <summary>
    /// Всё, связанное с коннектом к openvidu напрямую
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly OptionModel _optionModel;

        public PublishController(OptionModel optionModel)
        {
            _optionModel = optionModel;
        }

        /// <summary>
        ///     Генерирует токен для публикации
        /// </summary>
        [HttpPost]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequestModel model)
        {
            var path = "/api/tokens";
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Authorization", "Basic " + _optionModel.Secret);

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            request.AddParameter("application/json", JsonConvert.SerializeObject(model, serializerSettings),
                ParameterType.RequestBody);

            var response = JsonConvert.DeserializeObject<GenerateTokenResponseModel>(ExecuteRequest(path, request));

            return Ok(response);
        }

        private string ExecuteRequest(string path, RestRequest request)
        {
            var _client = new RestClient(_optionModel.OpenviduUrl + path);
            var response = _client.Execute(request);

            return response.Content;
        }
    }
}