using AutoMapper;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.WebApi.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.WebApi.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserOverviewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
