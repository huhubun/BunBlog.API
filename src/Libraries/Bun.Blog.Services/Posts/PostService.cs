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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Bun.Blog.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;

        public PostService(IRepository<Post> repository, IHttpContextAccessor httpContext)
        {
            _repository = repository;
        }

        public List<Post> GetAll()
        {
            return _repository.Table
                .Include(p => p.Author)
                .Include(p => p.Category)
                .OrderByDescending(p => p.InsertDate)
                .ToList();
        }

        public Post GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Post Add(Post post)
        {
            post.InsertDate = DateTime.Now;
            post.InsertUser = post.AuthorId;
            post.UpdateDate = DateTime.Now;
            post.UpdateUser = post.AuthorId;

            return _repository.Add(post);
        }

        public Post Update(Post post)
        {
            post.UpdateDate = DateTime.Now;
            post.UpdateUser = post.AuthorId;

            return _repository.Update(post);
        }

        public bool Exists(int id)
        {
            return _repository.Table.Any(post => post.Id == id);
        }
    }
}
