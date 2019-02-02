using System.Collections.Generic;
using System.Linq;
using ConsensusLibrary.CategoryContext.Exceptions;
using EnsureThat;

namespace ConsensusLibrary.CategoryContext
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            Ensure.Any.IsNotNull(category);
            Ensure.Bool.IsFalse(DoesCategoryExist(category.CategoryTitle), nameof(category),
                opt => opt.WithException(new CategoryAlreadyExistsException()));

            _categories.Add(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public bool DoesCategoryExist(string category)
        {
            Ensure.String.IsNotNullOrWhiteSpace(category);
            return _categories.Any(c => c.CategoryTitle.ToLower().Equals(category.ToLower()));
        }

        public Category GetCategoryByTitle(string title)
        {
            Ensure.String.IsNotNullOrWhiteSpace(title);
            var current = _categories.FirstOrDefault(c => c.CategoryTitle.ToLower().Equals(title.ToLower()));
            Ensure.Any.IsNotNull(current, nameof(current), opt => opt.WithException(new CategoryNotFoundException()));

            return current;
        }

        private readonly List<Category> _categories;

        public InMemoryCategoryRepository()
        {
            _categories = new List<Category>();
        }
    }
}
