using AutoMapper;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Web.Admin.Models.Categories;

namespace Bun.Blog.Web.Admin.Mappers
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryModel>();

            CreateMap<CategoryModel, Category>();
        }
    }
}
