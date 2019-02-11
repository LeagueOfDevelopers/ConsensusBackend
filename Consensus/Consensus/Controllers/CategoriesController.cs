using System.Collections.Generic;
using System.Linq;
using Consensus.Models.CategoriesModels;
using ConsensusLibrary.CategoryContext;
using EnsureThat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consensus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoriesController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = Ensure.Any.IsNotNull(categoryFacade);
        }

        /// <summary>
        /// Отобразить все возможные категории для дебатов
        /// </summary>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(GetAllCategoriesResponseModel), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetAllPosibleCategories()
        {
            var result = _categoryFacade.GetAllCategories();

            var items = new List<GetAllCategoriesResponseItemModel>();

            result.Categories.ToList().ForEach(c => items.Add(new GetAllCategoriesResponseItemModel(c.CategoryTitle)));

            var response = new GetAllCategoriesResponseModel(items);

            return Ok(response);
        }

    }
}
