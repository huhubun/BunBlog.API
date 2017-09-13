using AutoMapper;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.Web.Admin.Models.Categories;

namespace Bun.Blog.Web.Admin.Mappers
{
    public static class CategoryMapper
    {
        internal static IMapper Mapper { get; }

        static CategoryMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<CategoryMapperProfile>()).CreateMapper();
        }

        public static CategoryModel ToModel(this Category entity)
        {
            return Mapper.Map<CategoryModel>(entity);
        }

        public static Category ToEntity(this CategoryModel model)
        {
            return Mapper.Map<Category>(model);
        }
    }
}
