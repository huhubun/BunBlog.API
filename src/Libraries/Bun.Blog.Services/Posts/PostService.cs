using System;
using System.Collections.Generic;
using System.Text;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Data;
using System.Linq;
using Bun.Blog.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bun.Blog.Core.Domain.Users;
using System.Security.Claims;

namespace Bun.Blog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;

        public PostService(IRepository<Post> repository)
        {
            _repository = repository;
        }

        public IList<Post> GetAll()
        {
            return _repository.Table.Include(p => p.Author).ToList();
        }

        public Post GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Post Add(Post post)
        {
            return _repository.Add(post);
        }

        public Post Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
