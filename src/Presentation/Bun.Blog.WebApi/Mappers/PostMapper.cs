using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Mappers
{
    public static class PostMapper
    {
        internal static IMapper Mapper { get; }

        static PostMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PostMapperProfile>()).CreateMapper();
        }

        
    }
}
