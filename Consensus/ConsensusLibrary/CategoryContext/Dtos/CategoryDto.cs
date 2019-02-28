using System.ComponentModel.DataAnnotations;

namespace ConsensusLibrary.CategoryContext.Dtos
{
    public class CategoryDto
    {
        [Key]
        public string Title { get; set; }

        public CategoryDto(string title)
        {
            Title = title;
        }

        internal CategoryDto()
        {
        }
    }
}
