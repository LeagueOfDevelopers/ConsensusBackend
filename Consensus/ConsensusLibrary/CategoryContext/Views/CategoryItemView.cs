using System;
using System.Collections.Generic;
using System.Text;

namespace ConsensusLibrary.CategoryContext.Views
{
    public class CategoryItemView
    {
        public string CategoryTitle { get; }

        public CategoryItemView(string categoryTitle)
        {
            CategoryTitle = categoryTitle;
        }
    }
}
