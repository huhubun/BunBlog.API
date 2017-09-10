using AutoMapper;
using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Web.Admin.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Admin.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
