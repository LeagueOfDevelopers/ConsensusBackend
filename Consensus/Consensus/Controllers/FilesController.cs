using Consensus.Extensions;
using Consensus.Models.FileModels;
using ConsensusLibrary.FileContext;
using ConsensusLibrary.Tools;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public FilesController(IFileFacade fileFacade)
        {
            _fileFacade = Ensure.Any.IsNotNull(fileFacade);
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(PhysicalFileResult), 200)]
        public IActionResult GetFile([FromQuery] string fileName)
        {
            Ensure.String.IsNotNullOrWhiteSpace(fileName);

            var requestedUserId = Request.GetUserId();

            var currentFile = _fileFacade.GetFile(new Identifier(requestedUserId), fileName);

            return PhysicalFile(currentFile.PhysicalFileName, currentFile.FileType);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(UploadFileResponseModel), 200)]
        public IActionResult UploadFile(IFormFile file)
        {
            Ensure.Any.IsNotNull(file);

            var requestedUserId = Request.GetUserId();

            var newFileName = _fileFacade.AddFile(new Identifier(requestedUserId), file);

            var response = new UploadFileResponseModel(newFileName);

            return Ok(response);
        }

        private readonly IFileFacade _fileFacade;
    }
}