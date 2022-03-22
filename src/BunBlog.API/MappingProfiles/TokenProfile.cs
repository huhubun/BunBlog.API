using BunBlog.API.Models.Authentications;

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
