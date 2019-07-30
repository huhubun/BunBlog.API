using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunBlog.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public TagController()
        {

        }
        
        [HttpGet("")]
        public IActionResult GetList()
        {
            return Ok();
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