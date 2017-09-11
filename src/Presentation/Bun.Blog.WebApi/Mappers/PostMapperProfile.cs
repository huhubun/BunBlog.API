using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.WebApi.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Mappers
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            CreateMap<Post, PostListItem>();

            CreateMap<Post, PostDetailModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

        }
    }
}
