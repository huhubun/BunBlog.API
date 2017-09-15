using AutoMapper;
using Bun.Blog.Core.Domain.Categories;
using Bun.Blog.WebApi.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Mappers
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
