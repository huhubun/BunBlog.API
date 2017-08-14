using Bun.Blog.Core.Domain.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bun.Blog.Core.Domain.Users
{
    public class User : IdentityUser
    {
        public virtual ICollection<Post> Posts { get; set; }
    }
}
