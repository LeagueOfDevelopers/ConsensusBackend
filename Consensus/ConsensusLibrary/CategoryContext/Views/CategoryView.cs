using System.Collections.Generic;

namespace ConsensusLibrary.CategoryContext.Views
{
    public class CategoryView
    {
        public IEnumerable<CategoryItemView> Categories { get; }

        public CategoryView(IEnumerable<CategoryItemView> categories)
        {
            Categories = categories;
        }
    }
}
