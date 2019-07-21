namespace BunBlog.API.Models.Authentication
{
    public class TokenModel
    {
        public string AccessToken { get; set; }

        public uint ExpiresIn { get; set; }
    }
}
