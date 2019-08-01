using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Posts;
using BunBlog.API.Models.Tags;
using BunBlog.Core.Domain.Posts;
using BunBlog.Services.Categories;
using BunBlog.Services.Posts;
using BunBlog.Services.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public PostsController(
            IMapper mapper,
            IPostService postService,
            ICategoryService categoryService,
            ITagService tagService
            )
        {
            _mapper = mapper;
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        /// <summary>
        /// 获取博文列表
        /// </summary>
        /// <returns>博文列表</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetListAsync()
        {
            var posts = await _postService.GetListAsync();

            return Ok(_mapper.Map<List<BlogPostModel>>(posts));
        }

        /// <summary>
        /// 获取一条博文内容
        /// </summary>
        /// <param name="id">博文 id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            // 这里 noTracking 设为 false 是因为
            // Lazy-loading is not supported for detached entities or entities that are loaded with 'AsNoTracking()'.
            var post = await _postService.GetByIdAsync(id, noTracking: false);

            if (post == null)
            {
                return NotFound(new ErrorResponse(ErrorResponseCode.ID_NOT_FOUND, $"没有 id = {id} 的博文"));
            }

            return Ok(_mapper.Map<BlogPostModel>(post));
        }

        /// <summary>
        /// 创建一条博文
        /// </summary>
        /// <param name="createBlogPostModel">创建博文的请求</param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostAsync(CreateBlogPostModel createBlogPostModel)
        {
            var post = _mapper.Map<Post>(createBlogPostModel);

            if (!String.IsNullOrEmpty(createBlogPostModel.Category))
            {
                var category = await _categoryService.GetByLinkNameAsync(createBlogPostModel.Category);
                post.CategoryId = category.Id;
            }

            if (createBlogPostModel.Tags != null && createBlogPostModel.Tags.Any())
            {
                var tags = await _tagService.GetListByLinkNameAsync(createBlogPostModel.Tags.ToArray());
                post.PostTags = tags.Select(t => new PostTag
                {
                    TagId = t.Id
                }).ToList();
            }

            await _postService.PostAsync(post);

            return CreatedAtAction(nameof(GetAsync), new { id = post.Id }, _mapper.Map<BlogPostModel>(post));
        }
    }
}