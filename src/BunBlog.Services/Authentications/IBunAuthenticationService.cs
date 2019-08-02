using BunBlog.Core.Configuration;

namespace BunBlog.Services.Authentications
{
    public interface IBunAuthenticationService
    {
        AuthenticationUserConfig GetUser(string username, string password);

        string CreateToken(AuthenticationUserConfig user);
    }
}
