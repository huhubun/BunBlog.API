using AutoMapper;
using BunBlog.API.Models.Authentications;
using BunBlog.Services.Authentications;

namespace BunBlog.API.MappingProfiles
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<CreateTokenResult, TokenModel>();
        }
    }
}
