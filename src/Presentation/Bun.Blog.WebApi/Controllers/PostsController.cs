using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bun.Blog.Services.Posts;
using AutoMapper;
using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.WebApi.Models.Posts;

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

        [HttpGet, Route("{id:int}", Name = nameof(GetById))]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok("Get by id: " + id);
        }

    }
}
