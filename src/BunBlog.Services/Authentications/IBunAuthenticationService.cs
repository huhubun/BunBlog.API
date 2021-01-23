using BunBlog.Core.Configuration;

namespace BunBlog.Services.Authentications
{
    public interface IBunAuthenticationService
    {
        AuthenticationUserConfig GetUser(string username, string password);

        CreateTokenResult CreateToken(AuthenticationUserConfig user);

        bool IsRefreshTokenValid(string username, string refreshToken);

        CreateTokenResult CreateToken(string authorizationHeader, string refreshToken);

        void EndSession(string username, string authorizationHeader);

        bool CheckAlreadyEndSessionAccessToken(string username, string tokenSignature);
    }
}
