using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Services.Posts;
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
            CreateMap<Post, PostListItem>()
                .AfterMap((src, dest) =>
                {
                    dest.Visits = Convert.ToInt32(src?.Metas.SingleOrDefault(meta => meta.MetaKey == PostMetaKey.VISITS)?.MetaValue);
                });

            CreateMap<Post, PostDetailModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Metas, opt => opt.MapFrom(src => src.Metas.ToDictionary(m => m.MetaKey, m => m.MetaValue)));

        }
    }
}
