using EnsureThat;

namespace ConsensusLibrary.CategoryContext
{
    public class Category
    {
        public Category(string categoryTitle)
        {
            CategoryTitle = Ensure.String.IsNotNullOrWhiteSpace(categoryTitle);
        }

        public string CategoryTitle { get; }
    }
}
