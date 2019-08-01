using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BunBlog.API.Models.Categories;
using BunBlog.API.Models.Posts;
using BunBlog.API.Models.Tags;
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
        public PostsController()
        {

        }

        /// <summary>
        /// 获取博文列表
        /// </summary>
        /// <returns>博文列表</returns>
        [HttpGet("")]
        public IActionResult GetList()
        {
            return Ok(new List<BlogPostModel>
            {
                new BlogPostModel
                {
                    Title = "Test blog",
                    Visits = 2,
                    PublishedOn = new DateTime(2019, 7, 26, 12, 13, 14),
                    Tags = new List<TagModel>{ new TagModel {  LinkName = "a-tag", DisplayName = "AAA"}, new TagModel { LinkName = "test", DisplayName = "test" } },
                    Category = new CategoryModel{ LinkName="category", DisplayName = "My Category" }
                },
                new BlogPostModel
                {
                    Title = "222",
                    Visits = 0,
                    PublishedOn = new DateTime(2019, 7, 28, 17, 16, 15),
                    Tags = new List<TagModel>{ new TagModel {  LinkName = "a-tag", DisplayName = "AAA"}, new TagModel { LinkName = "2", DisplayName = "2" } },
                    Category = new CategoryModel{ LinkName="category", DisplayName = "My Category" }
                }
            });
        }

        /// <summary>
        /// 获取一条博文内容
        /// </summary>
        /// <param name="id">博文 id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new BlogPostModel
            {
                Title = "Test blog",
                Visits = 2,
                PublishedOn = new DateTime(2019, 7, 26, 12, 13, 14),
                Tags = new List<TagModel> { new TagModel { LinkName = "a-tag", DisplayName = "AAA" }, new TagModel { LinkName = "test", DisplayName = "test" } },
                Category = new CategoryModel { LinkName = "category", DisplayName = "My Category" }
            });
        }

        /// <summary>
        /// 创建一条博文
        /// </summary>
        /// <param name="model">创建博文的请求</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post(CreateBlogPostModel model)
        {
            //return Created($"~/{model.Title}", model);
            return Ok();
        }
    }
}