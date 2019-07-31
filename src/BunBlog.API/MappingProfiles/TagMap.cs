using AutoMapper;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.MappingProfiles
{
    public class TagMap : Profile
    {
        public TagMap()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<TagModel, Tag>();
        }
    }
}
