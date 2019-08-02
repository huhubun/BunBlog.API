using AutoMapper;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Posts;
using BunBlog.Core.Domain.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<PostTag, TagModel>().ConvertUsing((pt, tm, rc) =>
            {
                return rc.Mapper.Map<TagModel>(pt.Tag);
            });

            CreateMap<TagModel, Tag>();
        }
    }
}
