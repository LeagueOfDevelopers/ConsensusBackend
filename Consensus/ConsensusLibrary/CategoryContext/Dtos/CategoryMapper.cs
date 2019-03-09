namespace ConsensusLibrary.CategoryContext.Dtos
{
    public static class CategoryMapper
    {
        public static CategoryDto FromEntityToDto(this Category category)
        {
            var response = new CategoryDto(category.CategoryTitle);

            return response;
        }

        public static Category FromDtoToEntity(this CategoryDto category)
        {
            var response = new Category(category.Title);

            return response;
        }
    }
}
