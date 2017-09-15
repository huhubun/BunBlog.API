using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Enums;
using Bun.Blog.Web.Admin.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Admin.Mappers
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            CreateMap<Post, PostModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName));

            CreateMap<Post, PostListItem>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<PostModel, Post>()
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.Draft, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (src.Status == PostStatus.Published)
                    {
                        // 如果是发布文章，则将草稿的内容设为和发布的内容一致
                        dest.Draft = src.Content;
                        dest.Content = src.Content;
                    }
                    else
                    {
                        // 如果是保存草稿，则只更改数据库中的草稿列，不影响已发布的文章
                        dest.Draft = src.Content;
                    }
                });
        }
    }
}
