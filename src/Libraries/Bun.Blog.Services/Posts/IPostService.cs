using Bun.Blog.Core.Domain.Posts;
using System.Collections.Generic;

namespace Bun.Blog.Services.Posts
{
    public interface IPostService
    {
        List<Post> GetAll();

        Post GetById(int id);

        Post Add(Post post);

        Post Update(Post post);

        bool Exists(int id);
    }
}
