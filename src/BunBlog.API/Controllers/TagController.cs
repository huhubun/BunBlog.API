using BunBlog.API.Models.Posts;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IPostService _postService;

        public TagController(
            ITagService tagService,
            IMapper mapper,
            IPostService postService)
        {
            _tagService = tagService;
            _mapper = mapper;
            _postService = postService;
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
        public async Task<IActionResult> GetTagByLinkNameAsync([FromRoute] string linkName)
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
        [Authorize]
        public async Task<IActionResult> AddTagAsync([FromBody] TagModel tagModel)
        {
            if (ModelState.IsValid)
            {
                var tag = _mapper.Map<Tag>(tagModel);

                var isExistsResult = await _tagService.IsExistsAsync(tag);
                switch (isExistsResult)
                {
                    case IsTagExists.None:
                        break;

                    case IsTagExists.DisplayName:
                        return BadRequest(new ErrorResponse(ErrorResponseCode.DISPLAY_NAME_ALREADY_EXISTS, $"displayName \"{tagModel.DisplayName}\" 已存在"));

                    case IsTagExists.LinkName:
                        return BadRequest(new ErrorResponse(ErrorResponseCode.LINK_NAME_ALREADY_EXISTS, $"linkName \"{tagModel.LinkName}\" 已存在"));

                    default:
                        throw new InvalidOperationException($"意料外的枚举值 {isExistsResult.ToString()}");
                }

                await _tagService.AddAsync(tag);

                return CreatedAtRoute(nameof(GetTagByLinkNameAsync), new { linkName = tag.LinkName }, _mapper.Map<TagModel>(tag));
            }

            return UnprocessableEntity(ModelState);
        }

        /// <summary>
        /// 根据链接名称，修改指定标签
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <param name="tagModel">标签新的内容</param>
        /// <returns></returns>
        [HttpPut("{linkName}")]
        [Authorize]
        public async Task<IActionResult> EditTagByLinkNameAsync([FromRoute] string linkName, [FromBody] TagModel tagModel)
        {
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
        [Authorize]
        public async Task<IActionResult> DeleteTagByLinkNameAsync([FromRoute] string linkName)
        {
            var tag = await _tagService.GetByLinkNameAsync(linkName, tracking: true);

            if (tag == null)
            {
                return NotFound();
            }

            if (await _tagService.IsInUse(tag.Id))
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.IN_USE, $"标签 {tag.LinkName} 有博文使用，不能删除"));
            }

            await _tagService.DeleteAsync(tag);

            return NoContent();
        }

        [HttpGet("{linkName}/posts")]
        public async Task<IActionResult> GetPostsByTagAsync([FromRoute] string linkName)
        {
            var posts = await _postService.GetListByTagAsync(linkName);

            return Ok(_mapper.Map<List<BlogPostModel>>(posts));
        }
    }
}