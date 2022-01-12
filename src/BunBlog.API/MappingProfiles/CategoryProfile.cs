using BunBlog.API.Models.Categories;
using BunBlog.Core.Domain.Categories;

namespace BunBlog.API.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}
