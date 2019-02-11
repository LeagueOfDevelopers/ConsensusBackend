using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.CategoryContext.Views;
using EnsureThat;

namespace ConsensusLibrary.CategoryContext
{
    public class CategoryFacade : ICategoryFacade
    {
        public CategoryView GetAllCategories()
        {
            var all = _categoryRepository.GetAllCategories();

            var categoryItemView = new List<CategoryItemView>();

            all.ToList().ForEach(c => categoryItemView.Add(new CategoryItemView(c.CategoryTitle)));

            var result = new CategoryView(categoryItemView);

            return result;
        }

        public CategoryFacade(ICategoryRepository categoryRepository)
        {
            _categoryRepository = Ensure.Any.IsNotNull(categoryRepository);
        }

        private readonly ICategoryRepository _categoryRepository;
    }
}
