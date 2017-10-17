using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.WebApi.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Mappers
{
    public class PostMetaMapperProfile : Profile
    {
        public PostMetaMapperProfile()
        {
            CreateMap<PostMeta, PostMetaModel>();
            CreateMap<PostMetaModel, PostMeta>();
        }
    }
}
