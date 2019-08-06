using AutoMapper;
using BunBlog.API.Models.Categories;
using BunBlog.Core.Domain.Categories;
using BunBlog.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
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
            var category = new Category
            {
                LinkName = categoryModel.LinkName,
                DisplayName = categoryModel.DisplayName
            };

            await _categoryService.AddAsync(category);

            return CreatedAtRoute(nameof(GetCategoryByLinkNameAsync), new { linkName = category.LinkName }, _mapper.Map<CategoryModel>(category));
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

            await _categoryService.DeleteAsync(category);

            return NoContent();
        }
    }
}