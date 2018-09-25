using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consensus.Models;
using Consensus.Models.PublishModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Consensus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        public PublishController(OptionModel optionModel)
        {
            _optionModel = optionModel;
        }

        /// <summary>
        /// Генерирует токен для публикации
        /// </summary>
        [HttpPost]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequestModel model)
        {
            string path = "/api/tokens";
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Authorization", "Basic " + _optionModel.Secret);

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            request.AddParameter("application/json", JsonConvert.SerializeObject(model, serializerSettings), ParameterType.RequestBody);

            var response = JsonConvert.DeserializeObject<GenerateTokenResponseModel>(ExecuteRequest(path, request));

            return Ok(response);
        }

        private string ExecuteRequest(string path, RestRequest request)
        {
            var _client = new RestClient(_optionModel.OpenviduUrl + path);
            var response = _client.Execute(request);

            return response.Content;
        }

        private readonly OptionModel _optionModel;
    }
}