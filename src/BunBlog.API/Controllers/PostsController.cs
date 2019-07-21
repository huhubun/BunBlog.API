using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.API.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public PostsController()
        {

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { post_id = id, date  = DateTime.Now });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(PostBlogPostModel model)
        {
            //return Created($"~/{model.Title}", model);
            return Ok();
        }
    }
}