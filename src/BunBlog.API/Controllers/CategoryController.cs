using AutoMapper;
using BunBlog.API.Const;
using BunBlog.API.Models;
using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Posts;
using BunBlog.Core.Domain.Categories;
using BunBlog.Services.Categories;
using BunBlog.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunBlog.API.Controllers
{
    /// <summary>
    /// 分类
    /// </summary>
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper,
            IPostService postService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _postService = postService;
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetCategoryListAsync()
        {
            var categoryList = await _categoryService.GetListAsync();
            return Ok(_mapper.Map<List<CategoryModel>>(categoryList));
        }

        /// <summary>
        /// 根据链接名称，获取指定分类
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <returns></returns>
        [HttpGet("{linkName}", Name = nameof(GetCategoryByLinkNameAsync))]
        public async Task<IActionResult> GetCategoryByLinkNameAsync([FromRoute]string linkName)
        {
            var category = await _categoryService.GetByLinkNameAsync(linkName);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryModel>(category));
        }

        /// <summary>
        /// 添加一个分类
        /// </summary>
        /// <param name="categoryModel">添加分类请求</param>
        /// <returns></returns>
        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> AddCategoryAsync([FromBody]CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categoryModel);

                var isExistsResult = await _categoryService.IsExistsAsync(category);
                switch (isExistsResult)
                {
                    case IsCategoryExists.None:
                        break;

                    case IsCategoryExists.DisplayName:
                        return BadRequest(new ErrorResponse(ErrorResponseCode.DISPLAY_NAME_ALREADY_EXISTS, $"displayName \"{categoryModel.DisplayName}\" 已存在"));

                    case IsCategoryExists.LinkName:
                        return BadRequest(new ErrorResponse(ErrorResponseCode.LINK_NAME_ALREADY_EXISTS, $"linkName \"{categoryModel.LinkName}\" 已存在"));

                    default:
                        throw new InvalidOperationException($"意料外的枚举值 {isExistsResult.ToString()}");
                }

                await _categoryService.AddAsync(category);

                return CreatedAtRoute(nameof(GetCategoryByLinkNameAsync), new { linkName = category.LinkName }, _mapper.Map<CategoryModel>(category));
            }

            return UnprocessableEntity(ModelState);
        }

        /// <summary>
        /// 根据链接名称，修改指定分类
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <param name="categoryModel">分类新的内容</param>
        /// <returns></returns>
        [HttpPut("{linkName}")]
        [Authorize]
        public async Task<IActionResult> EditCategoryByLinkNameAsync([FromRoute]string linkName, [FromBody]CategoryModel categoryModel)
        {
            var category = await _categoryService.GetByLinkNameAsync(linkName, tracking: true);

            if (category == null)
            {
                return NotFound();
            }

            category = _mapper.Map(categoryModel, category);
            await _categoryService.EditAsync(category);

            return NoContent();
        }

        /// <summary>
        /// 根据链接名称，删除指定分类
        /// </summary>
        /// <param name="linkName">链接名称</param>
        /// <returns></returns>
        [HttpDelete("{linkName}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategoryByLinkNameAsync([FromRoute]string linkName)
        {
            var category = await _categoryService.GetByLinkNameAsync(linkName, tracking: true);

            if (category == null)
            {
                return NotFound();
            }

            if (await _categoryService.IsInUse(category.Id))
            {
                return BadRequest(new ErrorResponse(ErrorResponseCode.IN_USE, $"分类 {category.LinkName} 有博文使用，不能删除"));
            }

            await _categoryService.DeleteAsync(category);

            return NoContent();
        }

        [HttpGet("{linkName}/posts")]
        public async Task<IActionResult> GetPostsByCategoryAsync([FromRoute] string linkName)
        {
            var posts = await _postService.GetListByCategoryAsync(linkName);

            return Ok(_mapper.Map<List<BlogPostModel>>(posts));
        }
    }
}