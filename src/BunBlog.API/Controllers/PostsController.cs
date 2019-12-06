using AutoMapper;
using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Posts;
using BunBlog.Core.Domain.Posts;
using BunBlog.Services.Categories;
using BunBlog.Services.Posts;
using BunBlog.Services.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 博文
    /// </summary>
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IPostMetadataService _postMetadataService;

        public PostsController(
            IMapper mapper,
            IPostService postService,
            ICategoryService categoryService,
            ITagService tagService,
            IPostMetadataService postMetadataService
            )
        {
            _mapper = mapper;
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
            _postMetadataService = postMetadataService;
        }

        /// <summary>
        /// 获取博文列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns>博文列表</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<BlogPostListItemModel>), 200)]
        public async Task<IActionResult> GetListAsync(int page = 1, int pageSize = 10)
        {
            var posts = await _postService.GetListAsync(page, pageSize);

            return Ok(_mapper.Map<List<BlogPostListItemModel>>(posts));
        }

        /// <summary>
        /// 获取一条博文内容（通过Id）
        /// </summary>
        /// <param name="id">博文 id</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name =nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            // 这里 tracking 设为 true 是因为
            // Lazy-loading is not supported for detached entities or entities that are loaded with 'AsNoTracking()'.
            var post = await _postService.GetByIdAsync(id, tracking: true);

            if (post == null)
            {
                return NotFound(new ErrorResponse(ErrorResponseCode.ID_NOT_FOUND, $"没有 id 为 {id} 的博文"));
            }

            return Ok(_mapper.Map<BlogPostModel>(post));
        }

        /// <summary>
        /// 获取一条博文内容（通过链接名称）
        /// </summary>
        /// <param name="linkName">博文链接名称</param>
        /// <returns></returns>
        [HttpHead("{linkName}")]
        [HttpGet("{linkName}")]
        public async Task<IActionResult> GetByLinkNameAsync([FromRoute]string linkName)
        {
            var post = await _postService.GetByLinkNameAsync(linkName, tracking: true);

            if (post == null)
            {
                return NotFound(new ErrorResponse(ErrorResponseCode.ID_NOT_FOUND, $"没有链接名称为 {linkName} 的博文"));
            }

            return Ok(_mapper.Map<BlogPostModel>(post));
        }

        /// <summary>
        /// 创建一条博文
        /// </summary>
        /// <param name="createBlogPostModel">创建博文的请求</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody]CreateBlogPostModel createBlogPostModel)
        {
            var post = _mapper.Map<Post>(createBlogPostModel);

            if (!String.IsNullOrEmpty(createBlogPostModel.Category))
            {
                var category = await _categoryService.GetByLinkNameAsync(createBlogPostModel.Category, tracking: true);

                if (category == null)
                {
                    return BadRequest(new ErrorResponse(ErrorResponseCode.CATEGORY_NOT_EXISTS, $"分类 {createBlogPostModel.Category} 不存在"));
                }

                post.Category = category;
            }

            if (createBlogPostModel.TagList != null && createBlogPostModel.TagList.Any())
            {
                var tags = await _tagService.GetListByLinkNameAsync(tracking: true, createBlogPostModel.TagList.ToArray());

                if (createBlogPostModel.TagList.Count != tags.Count)
                {
                    var tagNames = tags.Select(t => t.LinkName);
                    var notExistsTags = createBlogPostModel.TagList.Where(t => !tagNames.Contains(t));

                    return BadRequest(new ErrorResponse(ErrorResponseCode.TAG_NOT_EXISTS, $"标签 {String.Join(", ", notExistsTags)} 不存在"));
                }

                post.TagList = tags.Select(t => new PostTag
                {
                    Tag = t
                }).ToList();
            }

            if (await _postService.LinkNameExists(post.LinkName, post.Type))
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.LINK_NAME_ALREADY_EXISTS, $"linkName \"{post.LinkName}\" 已存在"));
            }

            // set default value when title is empty
            if (String.IsNullOrEmpty(post.Title))
            {
                post.Title = DateTime.Now.ToString("yyyy-MM-dd");
            }

            await _postService.PostAsync(post);

            return CreatedAtRoute(nameof(GetAsync), new { id = post.Id }, _mapper.Map<BlogPostModel>(post));
        }

        /// <summary>
        /// 修改一条博文
        /// </summary>
        /// <param name="id">博文 id</param>
        /// <param name="editBlogPostModel">修改博文的请求</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromBody]EditBlogPostModel editBlogPostModel)
        {
            var post = await _postService.GetByIdAsync(id, tracking: true);

            if (post == null)
            {
                return NotFound();
            }

            post = _mapper.Map(editBlogPostModel, post);

            // Category
            if (!String.IsNullOrEmpty(editBlogPostModel.Category))
            {
                var category = await _categoryService.GetByLinkNameAsync(editBlogPostModel.Category, tracking: true);

                if (category == null)
                {
                    return BadRequest(new ErrorResponse(ErrorResponseCode.CATEGORY_NOT_EXISTS, $"分类 {editBlogPostModel.Category} 不存在"));
                }

                post.CategoryId = category.Id;
            }
            else
            {
                post.CategoryId = null;
            }

            // Tags
            if (editBlogPostModel.TagList.Any())
            {
                var tags = await _tagService.GetListByLinkNameAsync(tracking: true, editBlogPostModel.TagList.ToArray());

                if (editBlogPostModel.TagList.Count != tags.Count)
                {
                    var tagNames = tags.Select(t => t.LinkName);
                    var notExistsTags = editBlogPostModel.TagList.Where(t => !tagNames.Contains(t));

                    return BadRequest(new ErrorResponse(ErrorResponseCode.TAG_NOT_EXISTS, $"标签 {String.Join(", ", notExistsTags)} 不存在"));
                }

                var tagIds = tags.Select(t => t.Id);

                var currentTags = post.TagList.Where(t => tagIds.Contains(t.TagId)).ToList();
                var currentTagIds = currentTags.Select(t => t.TagId);

                foreach (var newTag in tags.Where(t => !currentTagIds.Contains(t.Id)))
                {
                    currentTags.Add(new PostTag
                    {
                        Tag = newTag
                    });
                }

                post.TagList = currentTags;
            }
            else
            {
                post.TagList = new List<PostTag>();
            }

            await _postService.EditAsync(post);

            return NoContent();
        }

        /// <summary>
        /// 为指定博文增加访问量
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}/visits")]
        public async Task<IActionResult> UpdateVisits([FromRoute]int id)
        {
            var metadata = await _postMetadataService.AddVisitsAsync(id);

            return Ok(_mapper.Map<PostMetadataModel>(metadata));
        }
    }
}