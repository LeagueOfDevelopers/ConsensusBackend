using System.Collections.Generic;

namespace Consensus.Models.CategoriesModels
{
    public class GetAllCategoriesResponseModel
    {
        public IEnumerable<GetAllCategoriesResponseItemModel> Categories { get; }

        public GetAllCategoriesResponseModel(IEnumerable<GetAllCategoriesResponseItemModel> categories)
        {
            Categories = categories;
        }
    }
}
