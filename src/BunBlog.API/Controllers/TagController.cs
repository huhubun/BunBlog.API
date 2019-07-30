using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunBlog.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly BunBlogContext _bunBlogContext;

        public TagController(BunBlogContext bunBlogContext)
        {
            _bunBlogContext = bunBlogContext;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _bunBlogContext.Tags.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}