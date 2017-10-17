using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bun.Blog.Services.Posts;
using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.WebApi.Models.Posts;
using Bun.Blog.WebApi.Filters;

namespace Bun.Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet, Route("")]
        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();

            return Ok(Mapper.Map<List<Post>, IEnumerable<PostListItem>>(posts));
        }

        [HttpGet, Route("{postId:int}", Name = nameof(GetById))]
        [ServiceFilter(typeof(PostVisitsFilter))]
        public IActionResult GetById([FromRoute] int postId)
        {
            var post = _postService.GetById(postId);

            return Ok(Mapper.Map<Post, PostDetailModel>(post));
        }
    }
}
