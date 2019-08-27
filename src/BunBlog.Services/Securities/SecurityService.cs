using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BunBlog.Services.Securities
{
    public class SecurityService : ISecurityService
    {
        public string Sha256(string content)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                var hash = sha256.ComputeHash(bytes);

                return HashToString(hash);
            }
        }

        private string HashToString(byte[] bytes)
        {
            return String.Join(String.Empty, bytes.Select(b => $"{b:X2}"));
        }
    }
}
