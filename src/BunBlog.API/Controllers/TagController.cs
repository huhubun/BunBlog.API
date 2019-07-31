using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Tags;
using BunBlog.Data;
using BunBlog.Services.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunBlog.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetTagListAsync()
        {
            return Ok(await _tagService.GetListAsync());
        }

        [HttpGet("{linkName}", Name = nameof(GetTagByLinkNameAsync))]
        public async Task<IActionResult> GetTagByLinkNameAsync([FromRoute]string linkName)
        {
            var tag = await _tagService.GetByLinkNameAsync(linkName);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddTagAsync([FromBody]TagModel tagModel)
        {
            var tag = new Tag
            {
                LinkName = tagModel.LinkName,
                Name = tagModel.Name
            };

            await _tagService.AddAsync(tag);

            return CreatedAtRoute(nameof(GetTagByLinkNameAsync), new { linkName = tag.LinkName }, tag);
        }

        [HttpPut("{linkName}")]
        public IActionResult EditTagByLinkName([FromRoute]string linkName, [FromBody]TagModel tagModel)
        {
            return Ok();
        }

        [HttpDelete("{linkName}")]
        public IActionResult DeleteTagByLinkName([FromRoute]string linkName)
        {
            return Ok();
        }
    }
}