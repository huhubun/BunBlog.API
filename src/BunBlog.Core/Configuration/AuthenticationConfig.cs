using System.Collections.Generic;

namespace BunBlog.Core.Configuration
{
    public class AuthenticationConfig
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public List<AuthenticationUserConfig> Users { get; set; }
    }

    public class AuthenticationUserConfig
    {
        public string Username { get; set; }

        public string Password { get; set; }

    }
}
