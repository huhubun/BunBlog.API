using AutoMapper;
using BunBlog.API.Models.Posts;
using BunBlog.Core.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.MappingProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<CreateBlogPostModel, Post>();
            CreateMap<Post, BlogPostModel>()
                .ForMember(m => m.Tags, mo => mo.MapFrom(p => p.PostTags));
                //.ForMember(m => m.Category, mo => mo.MapFrom(p => p.Category?.LinkName));
        }
    }
}
