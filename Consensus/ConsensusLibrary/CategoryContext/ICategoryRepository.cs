using System.Collections.Generic;

namespace ConsensusLibrary.CategoryContext
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        bool DoesCategoryExist(string category);
        Category GetCategoryByTitle(string title);
    }
}
