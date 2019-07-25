using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Models.Authentication
{
    public class CreateTokenRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
