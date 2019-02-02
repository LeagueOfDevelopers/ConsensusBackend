namespace Consensus.Models.CategoriesModels
{
    public class GetAllCategoriesResponseItemModel
    {
        public string CategoryTitle { get; }

        public GetAllCategoriesResponseItemModel(string categoryTitle)
        {
            CategoryTitle = categoryTitle;
        }
    }
}
