using System;
using System.Collections.Generic;
using System.Text;

namespace Bun.Blog.Services.Posts
{
    public interface IPostMetaService
    {
        void AddVisits(int postId);
    }
}
