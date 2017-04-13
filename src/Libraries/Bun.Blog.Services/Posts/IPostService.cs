using Bun.Blog.Core.Domain.Posts;
using System.Collections.Generic;

namespace Bun.Blog.Services.Posts
{
    public interface IPostService
    {
        IList<Post> GetAll();

        Post GetById(string id);

        Post Add(Post post);

        Post Update(Post post);
    }
}
