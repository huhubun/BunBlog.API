using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Tags;
using BunBlog.Data;
using BunBlog.Services.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 标签
    /// </summary>
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(
            ITagService tagService,
            IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetTagListAsync()
        {
            var tagList = await _tagService.GetListAsync();
            return Ok(_mapper.Map<List<TagModel>>(tagList));
        }

        /// <summary>
        /// 根据链接名称，获取指定标签
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <returns></returns>
        [HttpGet("{linkName}", Name = nameof(GetTagByLinkNameAsync))]
        public async Task<IActionResult> GetTagByLinkNameAsync([FromRoute]string linkName)
        {
            var tag = await _tagService.GetByLinkNameAsync(linkName);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TagModel>(tag));
        }

        /// <summary>
        /// 添加一个标签
        /// </summary>
        /// <param name="tagModel">添加标签请求</param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> AddTagAsync([FromBody]TagModel tagModel)
        {
            var tag = new Tag
            {
                LinkName = tagModel.LinkName,
                DisplayName = tagModel.DisplayName
            };

            await _tagService.AddAsync(tag);

            return CreatedAtRoute(nameof(GetTagByLinkNameAsync), new { linkName = tag.LinkName }, _mapper.Map<TagModel>(tag));
        }

        /// <summary>
        /// 根据链接名称，修改指定标签
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <param name="tagModel">标签新的内容</param>
        /// <returns></returns>
        [HttpPut("{linkName}")]
        public async Task<IActionResult> EditTagByLinkNameAsync([FromRoute]string linkName, [FromBody]TagModel tagModel)
        {
            if (linkName != tagModel.LinkName)
            {
                return BadRequest();
            }

            var tag = await _tagService.GetByLinkNameAsync(linkName, tracking: true);

            if (tag == null)
            {
                return NotFound();
            }

            tag = _mapper.Map(tagModel, tag);
            await _tagService.EditAsync(tag);

            return NoContent();
        }

        /// <summary>
        /// 根据链接名称，删除指定标签
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <returns></returns>
        [HttpDelete("{linkName}")]
        public async Task<IActionResult> DeleteTagByLinkNameAsync([FromRoute]string linkName)
        {
            var tag = await _tagService.GetByLinkNameAsync(linkName, tracking: true);

            if (tag == null)
            {
                return NotFound();
            }

            await _tagService.DeleteAsync(tag);

            return NoContent();
        }
    }
}