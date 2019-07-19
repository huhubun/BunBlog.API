using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}