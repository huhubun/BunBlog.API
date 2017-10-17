using Bun.Blog.Core.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bun.Blog.Services.Posts
{
    public interface IPostMetaService
    {
        List<PostMeta> GetList(int postId);

        PostMeta GetMeta(int postId, string metaKey);

        void AddVisits(int postId);
    }
}
